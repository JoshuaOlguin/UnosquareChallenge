using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AutomatedScript.Services
{
    public class Employee
    {
        public List<Employee> GetAllEmployees()
        {
            WebRequest req = WebRequest.Create("http://dummy.restapiexample.com/api/v1/employees");
            req.Method = "GET";
            req.ContentType = "application/json; charset=utf-8";
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader re = new StreamReader(stream);
            String json = re.ReadToEnd();

            return new List<Employee>();
        }

        public Employee GetEmployeeById(int id)
        {
            WebRequest req = WebRequest.Create("http://dummy.restapiexample.com/api/v1/employee/" + id.ToString());
            req.Method = "GET";
            req.ContentType = "application/json; charset=utf-8";
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader re = new StreamReader(stream);
            String json = re.ReadToEnd();

            return new Employee();
        }
    }
}
