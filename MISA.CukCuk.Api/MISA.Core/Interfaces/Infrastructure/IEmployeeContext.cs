using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Infrastructure
{
    public interface IEmployeeContext: IBaseRepository<Employee>
    {
       /// <summary>
       /// Lấy newEmployeeCode
       /// </summary>
       /// <returns></returns>
        string GetNewEmployeeCode();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        object GetEmployeesFilterPaging(int? pageSize, int? pageIndex, string employeeFilter, Guid? departmentId, Guid? positionId);
    }
}
