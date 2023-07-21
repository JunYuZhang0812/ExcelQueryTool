using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExcelQueryTool
{
    public partial class ExcelQuery : Form
    {
        private string m_excelPath;
        Regex regex = new Regex(@"^~\$");
        Regex m_regexTableName = new Regex(@".*(?=(data)$)",RegexOptions.IgnoreCase);
        private bool isCancel = false;
        public ExcelQuery()
        {
            InitializeComponent();
            InitPanel();
        }
        private List<string> m_fileList = new List<string>();
        private void InitPanel()
        {
            m_sliderProgress.Visible = false;
            m_textProgress.Visible = false;
            m_inputDirectory.Text = ExcelOP.DirectoryPath;
        }
        private void m_btnSelectDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹路径";
            dialog.SelectedPath = m_inputDirectory.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string str = dialog.SelectedPath;
                m_inputDirectory.Text = str;
            }
        }
        private void m_btnBegin_Click(object sender, EventArgs e)
        {
            if (!ExcelOP.Check()) return;
            if(m_asyncWorker.IsBusy )
            {
                isCancel = true;
                m_asyncWorker.CancelAsync();
                return;
            }
            m_excelPath = "";
            m_textExcelName.Text = "";
            m_fileList = ExcelOP.GetFilePathList();
            m_sliderProgress.Maximum = m_fileList.Count;
            m_sliderProgress.Minimum = 0;
            m_sliderProgress.Visible = true;
            m_textProgress.Visible = true;
            m_textProgress.Text = "开始加载Excel";
            m_togIsSVN.Enabled = false;
            m_togIsDepth.Enabled = false;
            isCancel = false;
            m_btnBegin.Text = "停止";
            m_asyncWorker.RunWorkerAsync();
        }

        private void m_btnOpenExcel_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(m_excelPath))
            {
                MessageBox.Show("未找到目标Excel");
                return;
            }
            var excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Open(m_excelPath);
            excel.Visible = true;
        }

        private void m_inputTableName_TextChanged(object sender, EventArgs e)
        {
            var str = m_inputTableName.Text;
            if (m_regexTableName.IsMatch(str))
                str = m_regexTableName.Match(str).Value;
            ExcelOP.TableName = str;
        }

        private void m_intputDirectory_TextChanged(object sender, EventArgs e)
        {
            ExcelOP.DirectoryPath = m_inputDirectory.Text;
            m_togIsSVN.Checked = ExcelOP.CheckIsSvnUrl(m_inputDirectory.Text);
        }

        private void m_asyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            m_excelPath = ExcelOP.CheckDicHasKey();
            if ( !string.IsNullOrEmpty(m_excelPath))
            {
                return;
            }
            for (int i = 0; i < m_fileList.Count; i++)
            {
                if (isCancel)
                    return;
                if(m_asyncWorker.WorkerReportsProgress)
                    m_asyncWorker.ReportProgress(i);
                var path = m_fileList[i];
                var code = ExcelOP.CheckHasTable(ref path);
                if (code == 1 )
                {
                    m_excelPath = path;
                    var result = MessageBox.Show("要查找的表在" + Path.GetFileName(m_excelPath) + "\r\n是否继续查找？", "查找结果", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes )
                    {
                        return;
                    }
                }
                else if(code == 0)
                {
                    //return;
                }
            }
        }
        private void m_asynWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            int currPro = e.ProgressPercentage;
            if (currPro > m_sliderProgress.Maximum)
                currPro = m_sliderProgress.Maximum;
            m_sliderProgress.Value = currPro;
            var str = Path.GetFileName( m_fileList[currPro] ) + "   " + Math.Floor(100d * currPro / m_sliderProgress.Maximum ) + "%";
            m_textProgress.Text = str;
        }
        private void m_asynWorker_Complete(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            m_sliderProgress.Visible = false;
            m_textProgress.Visible = false;
            m_togIsSVN.Enabled = true;
            m_togIsDepth.Enabled = true;
            m_btnBegin.Text = "查询";
            if (isCancel)
            {
                isCancel = false;
                MessageBox.Show("用户取消");
                return;
            }
            if (m_excelPath == null)
            {
                m_textExcelName.Text = "";
                MessageBox.Show("未找到对应Excel文件");
            }
            else
            {
                m_textExcelName.Text = Path.GetFileName(m_excelPath);
                //MessageBox.Show("要查找的表在"+Path.GetFileName(m_excelPath));
            }
        }

        private void ExcelQuery_DragEnter(object sender, DragEventArgs e)
        {
            // 对文件拖拽事件做处理 
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

        private void ExcelQuery_DragDrop(object sender, DragEventArgs e)
        {
            var filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (filePath.Length > 0)
            {
                var file = filePath[0];
                if (m_togIsSVN.Checked)
                {
                    file = SvnUtils.GetUri(file);
                }
                m_inputDirectory.Text = file;
            }
        }

        private void m_togIsSVN_CheckedChanged(object sender, EventArgs e)
        {
            ExcelOP.IsSvn = m_togIsSVN.Checked;
        }

        private void ExcelQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExcelOP.ClearFiles(TempFileOP.Instance.GetTempDirectoryPath());
        }

        private void m_inputTableName_MouseClick(object sender, MouseEventArgs e)
        {
            m_inputTableName.SelectAll();
        }

        private void m_inputDirectory_MouseClick(object sender, MouseEventArgs e)
        {
            m_inputDirectory.SelectAll();
        }

        private void m_togIsDepth_CheckedChanged(object sender, EventArgs e)
        {
            ExcelOP.IsDepth = m_togIsDepth.Checked;
        }
    }
}
