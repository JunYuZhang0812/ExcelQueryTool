using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ExcelQueryTool
{
    public class FileOP
    {
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="strSection">要写入的段落名</param>
        /// <param name="strKey">要写入的键</param>
        /// <param name="strValue">要写入的值</param>
        /// <param name="strFilePath">INI文件的完整路径和文件名</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string strSection, string strKey, string strValue, string strFilePath);
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="strSection">要读取的段落名</param>
        /// <param name="strKey">要读取的键</param>
        /// <param name="strDefine">读取异常的情况下的默认值</param>
        /// <param name="retValue">key所对应的值，如果该key不存在则返回空值</param>
        /// <param name="nSize">值允许的大小</param>
        /// <param name="strFilePath">INI文件的完整路径和文件名</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string strSection, string strKey, string strDefine, StringBuilder retValue, int nSize, string strFilePath);
        /// <summary>
        /// 文件路径
        /// </summary>
        private string m_filePath = string.Empty;
        public FileOP()
        {

        }
        public FileOP(string strFilePath)
        {
            SetFilePath(strFilePath);
        }
        /// <summary>
        /// 设置文件路径
        /// </summary>
        public void SetFilePath(string strFilePath)
        {
            m_filePath = strFilePath;
        }
        /// <summary>
        /// 读取文件路径
        /// </summary>
        public string GetFilePtah()
        {
            return m_filePath;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        public void CreateFile(string strFilePath)
        {
            SetFilePath(strFilePath);
            CreateFile();
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        public void CreateFile()
        {
            if (m_filePath.Length == 0) return;
            if (File.Exists(m_filePath)) return;
            FileStream fileStream = File.Create(m_filePath);
            if (fileStream != null)
                fileStream.Close();
        }
        /// <summary>
        /// 写入字符串
        /// </summary>
        public void WriteString(string strSection, string strKey, string strValue)
        {
            WritePrivateProfileString(strSection, strKey, strValue, m_filePath);
        }
        /// <summary>
        /// 写整形
        /// </summary>
        public void WriteInt(string strSection, string strKey, int intValue)
        {
            WriteString(strSection, strKey, intValue.ToString());
        }
        /// <summary>
        /// 写整形
        /// </summary>
        public void WriteInt(string strSection, string strKey, long intValue)
        {
            WriteString(strSection, strKey, intValue.ToString());
        }
        /// <summary>
        /// 写bool
        /// </summary>
        public void WriteBool(string strSection, string strKey, bool value)
        {
            string str = value ? "true" : "false";
            WriteString(strSection, strKey, str);
        }
        /// <summary>
        /// 读取字符串
        /// </summary>
        public string ReadString(string strSection, string strKey, string strDefine = "")
        {
            StringBuilder retValue = new StringBuilder(1024);
            int nSize = GetPrivateProfileString(strSection, strKey, strDefine, retValue, 1024, m_filePath);
            return retValue.ToString();
        }
        /// <summary>
        /// 读取int
        /// </summary>
        public int ReadInt(string strSection, string strKey, int intDefine = 0)
        {
            string str = ReadString(strSection, strKey, intDefine.ToString());
            int nValue = 0;
            int.TryParse(str, out nValue);
            return nValue;
        }
        /// <summary>
        /// 读取bool
        /// </summary>
        public bool ReadBool(string strSection, string strKey, bool value = false)
        {
            string strv = value ? "true" : "false";
            string str = ReadString(strSection, strKey, strv);
            return str.Equals("true");
        }
    }
}
