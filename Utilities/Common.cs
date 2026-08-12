using System;
using System.Reflection;
using System.Text;

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
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var physicalPath = Path.GetDirectoryName(assemblyLocation);

            // Skip from the end by taking all elements except the last 'skips' elements
            var pathSegments = physicalPath.Split('\\');
            var trimmedSegments = pathSegments.Take(pathSegments.Length - skips);
            physicalPath = String.Join(@"\", trimmedSegments);

            physicalPath = physicalPath + '\\' + relativePath;

            return physicalPath;
        }
    }
}
