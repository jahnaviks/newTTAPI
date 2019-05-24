using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TT_Common;
using TT_Services;

namespace TT_API.Controllers
{
    public class EmployeeController : ApiController
    {
        private IEmployee _iEmployee;
        public EmployeeController()
        {
            _iEmployee = new EmployeeService();
        }
        [HttpPost]
        [Route("api/Employee/Login")]
        public IHttpActionResult Login(Employee emp)
        {
            try
            {
                var result = _iEmployee.Login(emp);
                return Ok(result);
            }
            catch (Exception ex)
            {                
                throw;
            }            
        }

        [HttpPost]
        [Route("api/Employee/Register")]
        public bool Register([FromBody]Employee emp)
        {
            try
            {
                return _iEmployee.Register(emp);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpGet]
        [Route("api/Employee/EmployeeList")]
        public IHttpActionResult EmployeeList()
        {
            try
            {
                List<Employee> empList = new List<Employee>();
                empList = _iEmployee.EmployeeList();
                return Ok<List<Employee>>(empList);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpPost]
        [Route("api/Employee/EmployeeTeam")]
        public IHttpActionResult EmployeeTeam([FromBody]Employee emp)
        {
            try
            {
                List<Employee> empList = new List<Employee>();
                empList = _iEmployee.EmployeeTeam(emp);
                return Ok<List<Employee>>(empList);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpPost]
        [Route("api/Employee/EmployeeDetails")]
        public IHttpActionResult EmployeeDetails([FromBody]Employee emp)
        {
            try
            {
                //Employee emp = new Employee();
               // emp.email_Id = mail;
                var empList = _iEmployee.EmployeeDetails(emp);
                return Ok(empList);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/Employee/UpdateTeamMemberDetails")]
        public bool UpdateTeamMemberDetails([FromBody]Employee emp)
        {
            try
            {
                return _iEmployee.UpdateTeamMemberDetails(emp);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/Employee/UpdateSelfDetails")]
        public bool UpdateSelfDetails([FromBody]Employee emp)
        {
            try
            {
                return _iEmployee.UpdateSelfDetails(emp);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
