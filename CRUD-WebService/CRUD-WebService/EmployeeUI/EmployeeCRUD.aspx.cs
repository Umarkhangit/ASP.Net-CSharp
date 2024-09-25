using Azure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace CRUD_WebService.EmployeeUI
{
    public partial class EmployeeCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string SelectAll()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string url = "https://localhost:44392/Employees.asmx/SelectAllEmployees";
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string response = wc.UploadString(url, "POST");
                    return response;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public static string SelectById(int empId)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string url = "https://localhost:44392/Employees.asmx/SelectEmployeeById";
                    string parameter = "empId=" + empId;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string response = wc.UploadString(url,"POST",parameter);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public static string Insert(string empName, string empRole, string empSalary, string empMobile)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string url = "https://localhost:44392/Employees.asmx/InsertEmployee";
                    string parameter = "empName=" + empName + "&empRole=" + empRole + "&empSalary=" + empSalary + "&empMobile=" + empMobile;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string response = wc.UploadString(url,"POST",parameter);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public static string Update(int empId, string empName, string empRole, string empSalary, string empMobile)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string url = "https://localhost:44392/Employees.asmx/UpdateEmployee";
                    string parameter = "empId=" + empId +"&empName=" + empName + "&empRole=" + empRole + "&empSalary=" + empSalary + "&empMobile=" + empMobile;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string response = wc.UploadString(url, "POST", parameter);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public static string Delete(int empId)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string url = "https://localhost:44392/Employees.asmx/DeleteEmployee";
                    string parameter = "empId=" + empId;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string response = wc.UploadString(url, "POST", parameter);
                    return response;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}