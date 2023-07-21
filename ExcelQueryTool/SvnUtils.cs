using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SharpSvn;

namespace ExcelQueryTool
{
    class SvnUtils
    {
        private static System.Text.RegularExpressions.Regex m_regexUri = new System.Text.RegularExpressions.Regex(@".*(?=/$)");
        public static List<string> GetSvnPropertyList( string svnUrl )
        {
            List<string> paths = new List<string>();
            using (SvnClient client = new SvnClient())
            {
                Collection<SvnListEventArgs> list;
                SvnTarget tgt = SvnTarget.FromString(svnUrl);
                //SvnPropertyListArgs args = new SvnPropertyListArgs();
                client.GetList(tgt, out list);
                foreach (var node in list)
                {
                    if( !string.IsNullOrEmpty( node.Path ) )
                    {
                        var uri = node.Uri.ToString();
                        if(m_regexUri.IsMatch(uri))
                        {
                            uri = m_regexUri.Match(uri).Value;
                        }
                        paths.Add(uri);
                    }
                }
            }
            return paths;
        }
        public static string ExportUri(string uri)
        {
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnTarget tgt = SvnTarget.FromString(uri);
                    //SvnPropertyListArgs args = new SvnPropertyListArgs();
                    var path = TempFileOP.Instance.GetTempDirectoryPath();
                    var name = Path.GetFileName(uri);
                    //更新服务器有关联文件，要特殊处理
                    var localPath = path + "\\" + name;
                    if (!File.Exists(localPath))
                        client.Export(tgt, path);
                    return localPath;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "错误");
                }
            }
            return null;
        }
        public static string GetUri(string path)
        {
            try
            {
                using (SvnClient client = new SvnClient())
                {
                    Uri uri = client.GetUriFromWorkingCopy(path);
                    return uri.ToString();
                }
            }
            catch
            {

            }
            return "";
        }
    }
}
