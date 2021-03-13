using AutomatedScript.Models;
using System.Collections.Generic;

namespace AutomatedScript.Utilities.Files
{
    interface IFile
    {
        List<AmazonTestData> GetSearchForArrange(string path);
    }
}
