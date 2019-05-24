using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Common;

namespace TT_Services
{
    public interface IEmployee
    {
        bool Login(Employee emp);
        bool Register(Employee emp);
        List<Employee> EmployeeList();
        Employee EmployeeDetails(Employee emp);
        List<Employee> EmployeeTeam(Employee emp);
        bool UpdateSelfDetails(Employee emp);
        bool UpdateTeamMemberDetails(Employee emp);
    }
}
