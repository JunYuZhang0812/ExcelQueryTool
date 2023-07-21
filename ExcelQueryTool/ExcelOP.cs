using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExcelQueryTool
{
    public static class ExcelOP
    {
        private static FileOP m_cfgFile;
        private static Regex m_isUrlPath = new Regex(@"http://\d+\.\d+\.\d+\.\d+:\d+/svn/");
        private static Regex m_regexName = new Regex(@"^~\$");
        /// <summary>
        /// 配置文件
        /// </summary>
        public static FileOP CfgFile
        {
            get
            {
                if (m_cfgFile == null)
                {
                    m_cfgFile = new FileOP();
                    m_cfgFile.CreateFile(Application.StartupPath + "\\ExcelQueryConfig.ini");
                }
                return m_cfgFile;
            }
        }
        private static Regex regexTableName;
        public static bool IsSvn { get; set; } = false;
        public static bool IsDepth { get; set; } = false;
        private static string _TableName;
        public static string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
                regexTableName = new Regex( value,RegexOptions.IgnoreCase);
            }
        }
        private static string _DirectoryPath;
        public static string DirectoryPath {
            get
            {
                if(_DirectoryPath == null)
                {
                    _DirectoryPath = CfgFile.ReadString("Config", "DirectoryPath");
                }
                return _DirectoryPath;
            }
            set
            {
                if(value != _DirectoryPath)
                {
                    m_tableToExcelDic.Clear();
                    _DirectoryPath = value;
                    CfgFile.WriteString("Config", "DirectoryPath", _DirectoryPath);
                }
            }
        }
        private static string _ExcelExePath;
        public static string ExcelExePath
        {
            get
            {
                if (_ExcelExePath == null)
                {
                    _ExcelExePath = CfgFile.ReadString("Config", "_ExcelExePath");
                }
                if(string.IsNullOrEmpty(_ExcelExePath))
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Multiselect = false;
                    dialog.Title = "请选择Excel可执行程序";
                    dialog.Filter = string.Format("可执行程序(*.exe)|*.exe");
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ExcelExePath = dialog.FileName;
                    }
                }
                return _ExcelExePath;
            }
            set
            {
                _ExcelExePath = value;
                CfgFile.WriteString("Config", "_ExcelExePath", _ExcelExePath);
            }
        }
        private static Dictionary<string, string> m_tableToExcelDic = new Dictionary<string, string>();
        public static bool Check()
        {
            if( string.IsNullOrEmpty(DirectoryPath) )
            {
                MessageBox.Show("Excel文件夹路径错误");
                return false;
            }
            if( !IsSvn && !Directory.Exists(DirectoryPath) )
            {
                MessageBox.Show("Excel文件夹路径错误");
                return false;
            }
            if (string.IsNullOrEmpty(TableName) )
            {
                MessageBox.Show("Excel表名错误");
                return false;
            }
            if (IsSvn && !CheckIsSvnUrl(DirectoryPath))
            {
                MessageBox.Show("当前路径不是SVN路径");
                return false;
            }
            if (!IsSvn && CheckIsSvnUrl(DirectoryPath))
            {
                var result = MessageBox.Show("当前路径不是本地路径");
                return false;
            }
            return true;
        }
        public static bool CheckIsSvnUrl(string url)
        {
            return m_isUrlPath.IsMatch(url);
        }
        private static void AddSVNFilePath( List<string> files , string path )
        {
            var allFiles = SvnUtils.GetSvnPropertyList(path);
            for (int i = 0; i < allFiles.Count; i++)
            {
                var ex = Path.GetExtension(allFiles[i]);
                if( string.IsNullOrEmpty(ex) )
                {
                    AddSVNFilePath(files, allFiles[i]);
                }
                else if (ex.Equals(".xlsm") || ex.Equals(".xlsx") || ex.Equals(".xls") )
                {
                    files.Add(allFiles[i]);
                }
            }
        }
        public static List<string> GetFilePathList()
        {
            List<string> paths = new List<string>();
            if( IsSvn )
            {
                AddSVNFilePath(paths,DirectoryPath);
            }
            else
            {
                var arr = Directory.GetFiles(DirectoryPath, "*.xlsm", IsDepth ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                for (int i = 0; i < arr.Length; i++)
                {
                    paths.Add(arr[i]);
                }
            }
            List<string> fileList = new List<string>();
            for (int i = 0; i < paths.Count; i++)
            {
                if (!m_regexName.IsMatch(Path.GetFileName(paths[i])))
                {
                    fileList.Add(paths[i]);
                }
            }
            return fileList;
        }
        public static string CheckDicHasKey()
        {
            foreach (var item in m_tableToExcelDic)
            {
                if (ContainsKey(item.Key))
                    return item.Value;
            }
            return null;
        }
        public static int CheckHasTable( ref string excelPath )
        {
            if (IsSvn)
            {
                excelPath = SvnUtils.ExportUri(excelPath);
            }
            if (excelPath == null) return 0;
            IWorkbook workbook = null;
            try
            {
                using (FileStream file = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
                {
                    if (excelPath.EndsWith(".xls"))
                        workbook = new HSSFWorkbook(file);
                    else
                        workbook = new XSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("["+ excelPath+"]:"+e.ToString());
                return 0;
            }
            if (workbook != null)
            {
                for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
                {
                    try
                    {
                        var sheet = workbook.GetSheetAt(sheetIndex);
                        if (sheet != null)
                        {
                            if(!m_tableToExcelDic.ContainsKey(sheet.SheetName))
                                m_tableToExcelDic.Add(sheet.SheetName, excelPath);
                            if (ContainsKey( sheet.SheetName ) )
                                return 1;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            }
            return -1;
        }
        private static bool ContainsKey(string value)
        {
            return regexTableName.IsMatch(value);
        }
        public static void ClearFiles(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                var files = Directory.GetFiles(dirPath);
                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        File.Delete(files[i]);
                    }
                    catch { }
                }
                var dirs = Directory.GetDirectories(dirPath);
                for (int i = 0; i < dirs.Length; i++)
                {
                    ClearFiles(dirs[i]);
                }
                try
                {
                    Directory.Delete(dirPath);
                }
                catch { }
            }
        }
    }
}
