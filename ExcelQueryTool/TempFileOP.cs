using System.IO;
using System.Windows.Forms;

namespace ExcelQueryTool
{
    class TempFileOP
    {
        private static TempFileOP _Instance;
        public static TempFileOP Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new TempFileOP();
                return _Instance;
            }
        }
        private string m_tempDirPath;
        public TempFileOP()
        {
            m_tempDirPath = Application.StartupPath + "\\TempFile";
            ExcelOP.ClearFiles(m_tempDirPath);
            if ( !Directory.Exists(m_tempDirPath))
            {
                Directory.CreateDirectory(m_tempDirPath);
            }
        }
        public string GetTempDirectoryPath()
        {
            return m_tempDirPath;
        }
    }
}
