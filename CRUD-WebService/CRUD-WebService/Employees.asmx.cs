using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Azure;
using Newtonsoft.Json;

namespace CRUD_WebService
{
    /// <summary>
    /// Summary description for Employees
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Employees : System.Web.Services.WebService
    {
        [WebMethod]
        public void SelectAllEmployees()
        {
            Dictionary<string, object> errorObject = new Dictionary<string, object>();
            string response;
            try
            {
                EmployeeSQL empSql = new EmployeeSQL();
                var employeeList = empSql.SelectAll();

                if (employeeList.Count > 0)
                {
                    response = JsonConvert.SerializeObject(employeeList);
                }
                else
                {
                    errorObject.Add("Status", "Failed");
                    errorObject.Add("Description", "No Record found.");
                    response = JsonConvert.SerializeObject(errorObject);
                }
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
            catch (Exception ex)
            {
                errorObject.Add("Status", "Failed");
                errorObject.Add("Description", ex.Message);
                response = JsonConvert.SerializeObject(errorObject);
                HttpContext.Current.Response.Write(response);
            }
        }

        [WebMethod]
        public void SelectEmployeeById(int empId)
        {
            Dictionary<string, object> errorObject = new Dictionary<string, object>();
            string response;
            try
            {
                EmployeeSQL empSql = new EmployeeSQL();
                var empl = empSql.SelectById(empId);

                if (empl.Count > 0)
                {
                    response = JsonConvert.SerializeObject(empl);
                }
                else
                {
                    errorObject.Add("Status", "Failed");
                    errorObject.Add("Description", "Employee not found");
                    response = JsonConvert.SerializeObject(errorObject);
                }

                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
            catch (Exception ex)
            {
                errorObject.Add("Status", "Failed");
                errorObject.Add("Description", ex.Message);
                response = JsonConvert.SerializeObject(errorObject);

                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
        }

        [WebMethod]
        public void InsertEmployee(string empName, string empRole, int empSalary, string empMobile)
        {
            Dictionary<string, object> errorObject = new Dictionary<string, object>();
            string response;
            try
            {
                EmployeeSQL empSql = new EmployeeSQL
                {
                    EmpName = empName,
                    EmpRole = empRole,
                    EmpSalary = empSalary,
                    EmpMobile = empMobile
                };

                string status = empSql.Insert();
                if (status == "Pass")
                {
                    errorObject.Add("Status", "Success");
                    errorObject.Add("Description", "Employee details inserted successfully");
                    response = JsonConvert.SerializeObject(errorObject);
                }
                else
                {
                    errorObject.Add("Status", "Failed");
                    errorObject.Add("Description", status);
                    response = JsonConvert.SerializeObject(errorObject);
                }
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
            catch (Exception ex)
            {
                errorObject.Add("Status", "Failed");
                errorObject.Add("Description", ex);
                response = JsonConvert.SerializeObject(errorObject);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
        }

        [WebMethod]
        public void UpdateEmployee(int empId, string empName, string empRole, int empSalary, string empMobile)
        {
            Dictionary<string, object> errorObject = new Dictionary<string, object>();
            string response;
            try
            {
                EmployeeSQL empSql = new EmployeeSQL
                {
                    EmpId = empId,
                    EmpName = empName,
                    EmpRole = empRole,
                    EmpSalary = empSalary,
                    EmpMobile = empMobile
                };

                string status = empSql.Update();
                if (status == "Pass")
                {
                    errorObject.Add("Status", "Success");
                    errorObject.Add("Description", "Employee details updated successfully");
                    response = JsonConvert.SerializeObject(errorObject);
                }
                else
                {
                    errorObject.Add("Status", "Failed");
                    errorObject.Add("Description", status);
                    response =  JsonConvert.SerializeObject(errorObject);
                }
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
            catch (Exception ex)
            {
                errorObject.Add("Status", "Failed");
                errorObject.Add("Description", ex);
                response =  JsonConvert.SerializeObject(errorObject);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
        }

        [WebMethod]
        public void DeleteEmployee(int empId)
        {
            Dictionary<string, object> errorObject = new Dictionary<string, object>();
            string response;
            try
            {
                EmployeeSQL empSql = new EmployeeSQL();
                string status = empSql.Delete(empId);
                if (status == "Pass")
                {
                    errorObject.Add("Status", "Success");
                    errorObject.Add("Description", "Employee Deleted successfully");
                    response = JsonConvert.SerializeObject(errorObject);
                }
                else
                {
                    errorObject.Add("Status", "Failed");
                    errorObject.Add("Description", status);
                    response = JsonConvert.SerializeObject(errorObject);
                }
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
            catch (Exception ex)
            {
                errorObject.Add("Status", "Failed");
                errorObject.Add("Description", ex);
                response = JsonConvert.SerializeObject(errorObject);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Write(response);
            }
        }


        


        //[WebMethod]
        //public string SelectEmployeeById(int empId)
        //{
        //    Dictionary<string, object> errorObject = new Dictionary<string, object>();
        //    try
        //    {
        //        EmployeeSQL empSql = new EmployeeSQL();
        //        var empl = empSql.SelectById(empId);

        //        if (empl.Count > 0)
        //        {
        //            // Return employee data as JSON
        //            return JsonConvert.SerializeObject(empl);
        //        }
        //        else
        //        {
        //            // Return error if employee not found
        //            errorObject.Add("Status", "Failed");
        //            errorObject.Add("Description", "Employee not found");
        //            return JsonConvert.SerializeObject(errorObject);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return error in case of exception
        //        errorObject.Add("Status", "Failed");
        //        errorObject.Add("Description", ex.Message);
        //        return JsonConvert.SerializeObject(errorObject);
        //    }
        //}
    }
}
