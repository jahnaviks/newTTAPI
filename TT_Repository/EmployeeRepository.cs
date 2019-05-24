using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using TT_Common;

namespace TT_Repository
{
    public class EmployeeRepository
    {
        public bool Login(Employee emp)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.login_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", emp.email_Id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //emp.password = dr.GetValue(0).ToString();
                    //emp.roleId = dr.GetValue(1).ToString();
                    dr.Close();
                    con.Close();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public bool Register(Employee emp)
        {
            try
            {               
                if(GetEmployeeId(emp.email_Id) == null)
                {
                    emp.roleId = GetRoleId(emp.roleName);
                    if(!String.IsNullOrEmpty(emp.ae_email_Id))//emp.ae_email_Id == null)
                    {
                        emp.isActive = "1";
                    }
                    else
                    {
                        emp.isActive = "0";
                    }
                    SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                    con.Open();
                    SqlCommand cmd = new SqlCommand(GlobalConstants.register_SP, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", emp.ename);
                    cmd.Parameters.AddWithValue("@mobile", emp.mobile);
                    cmd.Parameters.AddWithValue("@password", emp.password);
                    cmd.Parameters.AddWithValue("@email", emp.email_Id);
                    cmd.Parameters.AddWithValue("@isActive", emp.isActive);
                    cmd.Parameters.AddWithValue("@roleId", emp.roleId);
                    cmd.Parameters.AddWithValue("@aeEmail", emp.ae_email_Id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    InsertProjectAllocation(emp);
                    return true;
                }
                else
                {
                    //employee already exist with given email id, please choose a different id.
                    return false;
                }
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void ApproveAllocation(Employee emp)
        {
            try
            {
                //InsertProjectAllocation(emp);
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.updateActiveStatus_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empId", emp.empId);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        public void InsertProjectAllocation(Employee emp)
        {
            try
            {
                emp.projectId = GetProjectId(emp.projectName);
                emp.empId = GetEmployeeId(emp.email_Id);
                //emp.roleId = GetRoleId(emp.roleName);
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.insertProjectAllocation_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@projId", emp.projectId);
                cmd.Parameters.AddWithValue("@empIdId", emp.empId);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                
                throw;
            }           
        }

        public string GetRoleId(string roleName)
        {
            string roleId = null;
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.getRoleId_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@roleName", roleName);
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    roleId = dr.GetValue(0).ToString();
                }
                dr.Close();
                con.Close();
                return roleId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetEmployeeId(string emailId)
        {
            string empId = null;
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.getEmpId_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", emailId);
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    empId = dr.GetValue(0).ToString();
                }
                dr.Close();
                con.Close();
                return empId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetProjectId(string projectName)
        {
            string projectId = null;
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.getProjectId_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@projName", projectName);
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    projectId = dr.GetValue(0).ToString();
                }
                dr.Close();
                con.Close();
                return projectId;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<Employee> EmployeeList()
        {
            try
            {
                List<Employee> empList = new List<Employee>();
                Employee empObj = new Employee();
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.getEmpListDetails_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    empList.Add(new Employee
                    {
                        empId = dr["EmployeeId"].ToString(),
                        ename = dr["Name"].ToString(),
                        mobile = dr["mobile"].ToString(),
                        email_Id = dr["EmailId"].ToString(),
                        isActive = dr["IsActive"].ToString(),
                        roleId = dr["RoleId"].ToString(),
                        projectId = dr["ProjectId"].ToString(),
                        projectName = dr["ProjectName"].ToString(),
                        roleName = dr["RoleName"].ToString(),
                        password = dr["password"].ToString(),
                        ae_email_Id = dr["AEEmailId"].ToString()
                    });
                }
                return empList;
            }
            catch (Exception)
            {               
                throw;
            }
        }
        public Employee EmployeeDetails(Employee emp)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.getEmpDetails_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", emp.email_Id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    emp.empId = dr["EmployeeId"].ToString();
                    emp.ename = dr["Name"].ToString();
                    emp.mobile = dr["mobile"].ToString();
                    emp.email_Id = dr["EmailId"].ToString();
                    emp.isActive = dr["IsActive"].ToString();
                    emp.roleId = dr["RoleId"].ToString();
                    emp.projectId = dr["ProjectId"].ToString();
                    emp.projectName = dr["ProjectName"].ToString();
                    emp.roleName = dr["RoleName"].ToString();
                    emp.password = dr["password"].ToString();
                    emp.ae_email_Id = dr["AEEmailId"].ToString();
                }
                dr.Close();
                con.Close();
                return emp;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<Employee> getTeamDetails(Employee emp)
        {
            try
            {
                //emp.projectId = GetProjectId(emp.projectName);
                List<Employee> empList = new List<Employee>();
                Employee empObj = new Employee();
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.getTeamDetails_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@roleId", emp.roleId);
                cmd.Parameters.AddWithValue("@projId", emp.projectId);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    empList.Add(new Employee
                    {
                        empId = dr["EmployeeId"].ToString(),
                        ename = dr["Name"].ToString(),
                        mobile = dr["mobile"].ToString(),
                        email_Id = dr["EmailId"].ToString(),
                        isActive = dr["IsActive"].ToString(),
                        roleId = dr["RoleId"].ToString(),
                        projectId = dr["ProjectId"].ToString(),
                        projectName = dr["ProjectName"].ToString(),
                        roleName = dr["RoleName"].ToString(),
                        ae_email_Id = dr["AEEmailId"].ToString()
                        //password = dr["password"].ToString(),
                    });
                }
                return empList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateSelfDetails(Employee emp)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.updateSelfDetails_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empId", emp.empId);
                cmd.Parameters.AddWithValue("@name", emp.ename);
                cmd.Parameters.AddWithValue("@mobile", emp.mobile);
                cmd.Parameters.AddWithValue("@password", emp.password);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateTeamMemberDetails(Employee emp)
        {
            try
            {
                emp.projectId = GetProjectId(emp.projectName);
                emp.roleId = GetRoleId(emp.roleName);
                SqlConnection con = new SqlConnection(ConnectionString.GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(GlobalConstants.updateTeamMemberDetails_SP, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empId", emp.empId);
                cmd.Parameters.AddWithValue("@rId", emp.roleId);
                cmd.Parameters.AddWithValue("@isActive", emp.isActive);
                cmd.Parameters.AddWithValue("@aeEmail", emp.ae_email_Id);
                cmd.Parameters.AddWithValue("@projectId", emp.projectId);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
