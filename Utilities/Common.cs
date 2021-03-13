using System;
using System.Linq;
using System.Text;
using System.IO;

namespace AutomatedScript.Utilities
{
    public class Common
    {
        public string ReadFile(string path)
        {
            string result = string.Empty;

            string[] lines = File.ReadAllLines(@path);
            result = ConvertStringArrayToString(lines);

            return result;
        }

        private string ConvertStringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string value in array)
            {
                builder.Append(value);
            }

            return builder.ToString();
        }

        public string GetPhysicalPathByRelative(string relativePath)
        {
            var physicalPath = Path.Combine(Directory.GetParent(@"../../").FullName, relativePath);

            return physicalPath;
        }
        public string GetPhysicalPathByRelative(string relativePath, int skips)
        {
            var physicalPath = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            physicalPath = String.Join(@"\", physicalPath.Split('\\').Reverse().Skip(skips).Reverse());
            physicalPath = physicalPath + '\\' + relativePath;

            return physicalPath;
        }
    }
}
