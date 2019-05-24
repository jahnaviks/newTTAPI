using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Common;
using TT_Repository;

namespace TT_Services
{
    public class EmployeeService : IEmployee
    {

        public bool Login(Employee emp)
        {
            try
            {
                EmployeeRepository emp_Repository = new EmployeeRepository();
                var result = emp_Repository.Login(emp);
                return result;
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
                EmployeeRepository emp_Repository = new EmployeeRepository();
                return emp_Repository.Register(emp);
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
                EmployeeRepository emp_Repository = new EmployeeRepository();
                return emp_Repository.EmployeeList();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<Employee> EmployeeTeam(Employee emp)
        {
            try
            {
                EmployeeRepository emp_Repo = new EmployeeRepository();
                return emp_Repo.getTeamDetails(emp);
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
                EmployeeRepository emp_Repository = new EmployeeRepository();
                return emp_Repository.EmployeeDetails(emp);
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
                EmployeeRepository emp_Repository = new EmployeeRepository();
                return emp_Repository.UpdateSelfDetails(emp);
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
                EmployeeRepository emp_Repository = new EmployeeRepository();
                return emp_Repository.UpdateTeamMemberDetails(emp);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
