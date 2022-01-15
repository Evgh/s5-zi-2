using System.IO;

namespace CummonLogic
{
    public static class ExtensionsToWorkWithFiles
    {
        public static string ReadAsFileSourceToEnd(this string path)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }

        public static string ReadAsFileSourceFromProjectRoot(this string path)
        {
            string root = GetProjectRootPath();
            string fullpath = $"{root}/{path}";

            return ReadAsFileSourceToEnd(fullpath);
        }

        public static string GetProjectRootPath()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        }
    }
}
