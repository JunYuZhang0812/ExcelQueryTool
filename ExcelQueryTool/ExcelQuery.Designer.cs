namespace ExcelQueryTool
{
    partial class ExcelQuery
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelQuery));
            this.m_inputTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnBegin = new System.Windows.Forms.Button();
            this.m_inputDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_textExcelName = new System.Windows.Forms.TextBox();
            this.m_btnOpenExcel = new System.Windows.Forms.Button();
            this.m_btnSelectDirectory = new System.Windows.Forms.Button();
            this.m_asyncWorker = new System.ComponentModel.BackgroundWorker();
            this.m_sliderProgress = new System.Windows.Forms.ProgressBar();
            this.m_textProgress = new System.Windows.Forms.Label();
            this.m_togIsSVN = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_togIsDepth = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_inputTableName
            // 
            this.m_inputTableName.Location = new System.Drawing.Point(50, 62);
            this.m_inputTableName.Name = "m_inputTableName";
            this.m_inputTableName.Size = new System.Drawing.Size(268, 21);
            this.m_inputTableName.TabIndex = 0;
            this.m_inputTableName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_inputTableName_MouseClick);
            this.m_inputTableName.TextChanged += new System.EventHandler(this.m_inputTableName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "表名：";
            // 
            // m_btnBegin
            // 
            this.m_btnBegin.Location = new System.Drawing.Point(324, 60);
            this.m_btnBegin.Name = "m_btnBegin";
            this.m_btnBegin.Size = new System.Drawing.Size(75, 23);
            this.m_btnBegin.TabIndex = 2;
            this.m_btnBegin.Text = "查询";
            this.m_btnBegin.UseVisualStyleBackColor = true;
            this.m_btnBegin.Click += new System.EventHandler(this.m_btnBegin_Click);
            // 
            // m_inputDirectory
            // 
            this.m_inputDirectory.Location = new System.Drawing.Point(50, 32);
            this.m_inputDirectory.Name = "m_inputDirectory";
            this.m_inputDirectory.Size = new System.Drawing.Size(268, 21);
            this.m_inputDirectory.TabIndex = 3;
            this.m_inputDirectory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_inputDirectory_MouseClick);
            this.m_inputDirectory.TextChanged += new System.EventHandler(this.m_intputDirectory_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "目录：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "所在Excel表：";
            // 
            // m_textExcelName
            // 
            this.m_textExcelName.Location = new System.Drawing.Point(90, 89);
            this.m_textExcelName.Name = "m_textExcelName";
            this.m_textExcelName.Size = new System.Drawing.Size(228, 21);
            this.m_textExcelName.TabIndex = 6;
            // 
            // m_btnOpenExcel
            // 
            this.m_btnOpenExcel.Location = new System.Drawing.Point(324, 90);
            this.m_btnOpenExcel.Name = "m_btnOpenExcel";
            this.m_btnOpenExcel.Size = new System.Drawing.Size(75, 23);
            this.m_btnOpenExcel.TabIndex = 7;
            this.m_btnOpenExcel.Text = "打开";
            this.m_btnOpenExcel.UseVisualStyleBackColor = true;
            this.m_btnOpenExcel.Click += new System.EventHandler(this.m_btnOpenExcel_Click);
            // 
            // m_btnSelectDirectory
            // 
            this.m_btnSelectDirectory.Location = new System.Drawing.Point(324, 31);
            this.m_btnSelectDirectory.Name = "m_btnSelectDirectory";
            this.m_btnSelectDirectory.Size = new System.Drawing.Size(75, 23);
            this.m_btnSelectDirectory.TabIndex = 8;
            this.m_btnSelectDirectory.Text = "选择文件夹";
            this.m_btnSelectDirectory.UseVisualStyleBackColor = true;
            this.m_btnSelectDirectory.Click += new System.EventHandler(this.m_btnSelectDirectory_Click);
            // 
            // m_asyncWorker
            // 
            this.m_asyncWorker.WorkerReportsProgress = true;
            this.m_asyncWorker.WorkerSupportsCancellation = true;
            this.m_asyncWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_asyncWorker_DoWork);
            this.m_asyncWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.m_asynWorker_ProgressChanged);
            this.m_asyncWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_asynWorker_Complete);
            // 
            // m_sliderProgress
            // 
            this.m_sliderProgress.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_sliderProgress.Location = new System.Drawing.Point(2, 134);
            this.m_sliderProgress.Name = "m_sliderProgress";
            this.m_sliderProgress.Size = new System.Drawing.Size(396, 23);
            this.m_sliderProgress.TabIndex = 9;
            // 
            // m_textProgress
            // 
            this.m_textProgress.AutoSize = true;
            this.m_textProgress.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_textProgress.Location = new System.Drawing.Point(1, 119);
            this.m_textProgress.Name = "m_textProgress";
            this.m_textProgress.Size = new System.Drawing.Size(95, 12);
            this.m_textProgress.TabIndex = 13;
            this.m_textProgress.Text = "---------------";
            // 
            // m_togIsSVN
            // 
            this.m_togIsSVN.AutoSize = true;
            this.m_togIsSVN.Location = new System.Drawing.Point(63, 8);
            this.m_togIsSVN.Name = "m_togIsSVN";
            this.m_togIsSVN.Size = new System.Drawing.Size(42, 16);
            this.m_togIsSVN.TabIndex = 14;
            this.m_togIsSVN.Text = "SVN";
            this.m_togIsSVN.UseVisualStyleBackColor = true;
            this.m_togIsSVN.CheckedChanged += new System.EventHandler(this.m_togIsSVN_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(111, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "提示：[Depth]检索全部子级。[SVN]检索SVN文件夹。";
            // 
            // m_togIsDepth
            // 
            this.m_togIsDepth.AutoSize = true;
            this.m_togIsDepth.Location = new System.Drawing.Point(3, 7);
            this.m_togIsDepth.Name = "m_togIsDepth";
            this.m_togIsDepth.Size = new System.Drawing.Size(54, 16);
            this.m_togIsDepth.TabIndex = 16;
            this.m_togIsDepth.Text = "Depth";
            this.m_togIsDepth.UseVisualStyleBackColor = true;
            this.m_togIsDepth.CheckedChanged += new System.EventHandler(this.m_togIsDepth_CheckedChanged);
            // 
            // ExcelQuery
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 162);
            this.Controls.Add(this.m_togIsDepth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_togIsSVN);
            this.Controls.Add(this.m_textProgress);
            this.Controls.Add(this.m_sliderProgress);
            this.Controls.Add(this.m_btnSelectDirectory);
            this.Controls.Add(this.m_btnOpenExcel);
            this.Controls.Add(this.m_textExcelName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_inputDirectory);
            this.Controls.Add(this.m_btnBegin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_inputTableName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(420, 201);
            this.Name = "ExcelQuery";
            this.Text = "Excel查询工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExcelQuery_FormClosed);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ExcelQuery_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ExcelQuery_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_inputTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnBegin;
        private System.Windows.Forms.TextBox m_inputDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_textExcelName;
        private System.Windows.Forms.Button m_btnOpenExcel;
        private System.Windows.Forms.Button m_btnSelectDirectory;
        private System.ComponentModel.BackgroundWorker m_asyncWorker;
        private System.Windows.Forms.ProgressBar m_sliderProgress;
        private System.Windows.Forms.Label m_textProgress;
        private System.Windows.Forms.CheckBox m_togIsSVN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox m_togIsDepth;
    }
}

