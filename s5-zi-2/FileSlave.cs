using System.IO;

namespace s5_zi_2
{
    public static class FileSlave
    {
        public static string ReadFileToEnd(string path)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
