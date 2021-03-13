using AutomatedScript.Models;
using AutomatedScript.Utilities.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedScript.Utilities
{
    public class JsonFile : IFile
    {
        public List<AmazonTestData> GetSearchForArrange(string path)
        {
            List<AmazonTestData> result = new List<AmazonTestData>();
            var data = new Common().ReadFile(path);

            result = JsonConvert.DeserializeObject<List<AmazonTestData>>(data);
            return result;
        }
    }
}
