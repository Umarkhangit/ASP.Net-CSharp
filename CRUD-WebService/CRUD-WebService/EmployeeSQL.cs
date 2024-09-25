using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Net.Http;
using System.IO;

namespace CRUD_WebService
{
    public class EmployeeSQL
    {
        private int empId;
        private string empName;
        private string empRole;
        private int empSalary;
        private string empMobile;

        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }

        public string EmpRole
        {
            get { return empRole; }
            set { empRole = value; }
        }

        public int EmpSalary
        {
            get { return empSalary; }
            set { empSalary = value; }
        }

        public string EmpMobile
        {
            get { return empMobile; }
            set { empMobile = value; }
        }


        // select by Id method
        public List<Dictionary<string, object>> SelectById(int id)
        {
            var employees = new List<Dictionary<string, object>>();
            try
            {
                DataSetsTableAdapters.SelectEmployeeByIdTableAdapter selectEmployeesTableAdapter = new DataSetsTableAdapters.SelectEmployeeByIdTableAdapter();
                DataTable table = selectEmployeesTableAdapter.GetData(id);

                if(table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    var emp = new Dictionary<string, object>
                    {
                        { "EmpID", Convert.ToInt32(row["EmpID"]) },
                        { "EmpName", row["EmpName"].ToString() },
                        { "EmpRole", row["EmpRole"].ToString() },
                        { "EmpSalary", Convert.ToInt32(row["EmpSalary"]) },
                        { "EmpMobile", row["EmpMobile"].ToString() }
                    };
                    employees.Add(emp);
                }
            }
            catch (Exception ex)
            {
                var error = new Dictionary<string, object>
                {
                    { "Error", ex.Message }
                };
                employees.Add(error);
            }
            return employees;
        }

        // select all method
        public List<Dictionary<string, object>> SelectAll()
        {
            var employees = new List<Dictionary<string, object>>();
            try
            {
                DataSetsTableAdapters.SelectAllEmployeesTableAdapter ta = new DataSetsTableAdapters.SelectAllEmployeesTableAdapter();
                DataTable table = ta.GetData();

                if(table != null && table.Rows.Count > 0)
                {
                    DataRowCollection rows = table.Rows;
                    foreach (DataRow row in rows)
                    {
                        var emp = new Dictionary<string, object>
                        {
                            { "EmpID", Convert.ToInt32(row["EmpID"]) },
                            { "EmpName", row["EmpName"].ToString() },
                            { "EmpRole", row["EmpRole"].ToString() },
                            { "EmpSalary", Convert.ToInt32(row["EmpSalary"]) },
                            { "EmpMobile", row["EmpMobile"].ToString() }
                        };
                        employees.Add(emp);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = new Dictionary<string, object>
                {
                    { "Error", ex.Message }
                };
                employees.Add(error);
            }
            return employees;
        }

        // insert method
        public string Insert()
        {
            try
            {
                DataSetsTableAdapters.QueriesTableAdapter qa = new DataSetsTableAdapters.QueriesTableAdapter();
                int count = qa.InsertEmployees(EmpName,EmpRole,EmpSalary,EmpMobile);
                return count > 0 ? "Pass" : "Fail";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // update method
        public string Update()
        {
            try
            {
                DataSetsTableAdapters.QueriesTableAdapter queriesTableAdapter = new DataSetsTableAdapters.QueriesTableAdapter();
                int count = queriesTableAdapter.UpdateEmployee(EmpId, EmpName, EmpRole, EmpSalary, EmpMobile);
                return count > 0 ? "Pass" : "Fail";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // delete method
        public string Delete(int id)
        {
            try
            {
                DataSetsTableAdapters.QueriesTableAdapter qa = new DataSetsTableAdapters.QueriesTableAdapter();
                int count = qa.DeleteEmployee(id);
                return count > 0 ? "Pass" : "Fail";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        //private readonly string connectionStr = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        //public List<Dictionary<string, object>> Select(int Id)
        //{
        //    var employees = new List<Dictionary<string, object>>();

        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(connectionStr))
        //        {
        //            using (SqlCommand sqlCmd = new SqlCommand("SelectEmployeeById", sqlCon))
        //            {
        //                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                sqlCmd.Parameters.AddWithValue("@eid", Id);
        //                sqlCon.Open();

        //                using (SqlDataReader reader = sqlCmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var emp = new Dictionary<string, object>
        //                        {
        //                            { "EmpID", Convert.ToInt32(reader["EmpID"]) },
        //                            { "EmpName", reader["EmpName"].ToString() },
        //                            { "EmpRole", reader["EmpRole"].ToString() },
        //                            { "EmpSalary", Convert.ToInt32(reader["EmpSalary"]) },
        //                            { "EmpMobile", reader["EmpMobile"].ToString() }
        //                        };
        //                        employees.Add(emp);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = new Dictionary<string, object>
        //        {
        //            { "Error", ex.Message }
        //        };
        //        employees.Add(error);
        //    }
        //    return employees;
        //}

        //public List<Dictionary<string, object>> SelectAll()
        //{
        //    var employees = new List<Dictionary<string, object>>();

        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(connectionStr))
        //        {
        //            using (SqlCommand sqlCmd = new SqlCommand("SelectAllEmployees", sqlCon))
        //            {
        //                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                sqlCon.Open();

        //                using (SqlDataReader reader = sqlCmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var emp = new Dictionary<string, object>
        //                        {
        //                            { "EmpID", Convert.ToInt32(reader["EmpID"]) },
        //                            { "EmpName", reader["EmpName"].ToString() },
        //                            { "EmpRole", reader["EmpRole"].ToString() },
        //                            { "EmpSalary", Convert.ToInt32(reader["EmpSalary"]) },
        //                            { "EmpMobile", reader["EmpMobile"].ToString() }
        //                        };
        //                        employees.Add(emp);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = new Dictionary<string, object>
        //        {
        //            { "Error", ex.Message }
        //        };
        //        employees.Add(error);
        //    }
        //    return employees;
        //}

        //public string Insert()
        //{

        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(connectionStr))
        //        {
        //            using (SqlCommand sqlCmd = new SqlCommand("InsertEmployees", sqlCon))
        //            {
        //                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                sqlCmd.Parameters.AddWithValue("@ename", EmpName);
        //                sqlCmd.Parameters.AddWithValue("@erole", EmpRole);
        //                sqlCmd.Parameters.AddWithValue("@esalary", EmpSalary);
        //                sqlCmd.Parameters.AddWithValue("@emobile", EmpMobile);

        //                sqlCon.Open();
        //                int count = sqlCmd.ExecuteNonQuery();

        //                return count > 0 ? "Pass" : "Fail";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        //public string Update()
        //{
        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(connectionStr))
        //        {
        //            using (SqlCommand sqlCmd = new SqlCommand("UpdateEmployee", sqlCon))
        //            {
        //                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                sqlCmd.Parameters.AddWithValue("@eid", EmpId);
        //                sqlCmd.Parameters.AddWithValue("@ename", EmpName);
        //                sqlCmd.Parameters.AddWithValue("@erole", EmpRole);
        //                sqlCmd.Parameters.AddWithValue("@esalary", EmpSalary);
        //                sqlCmd.Parameters.AddWithValue("@emobile", EmpMobile);

        //                sqlCon.Open();
        //                int count = sqlCmd.ExecuteNonQuery();

        //                return count > 0 ? "Pass" : "Fail";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        //public string Delete(int id)
        //{
        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(connectionStr))
        //        {
        //            using (SqlCommand sqlCmd = new SqlCommand("DeleteEmployee", sqlCon))
        //            {
        //                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                sqlCmd.Parameters.AddWithValue("@eid", id);

        //                sqlCon.Open();
        //                int count = sqlCmd.ExecuteNonQuery();

        //                return count > 0 ? "Pass" : "Fail";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}