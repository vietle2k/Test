using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// Lấy Mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        public string GetNewEmployeeCode();
        /// <summary>
        /// lay employee phan trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public object GetEmployeesFilterPaging(int? pageSize, int? pageIndex, string employeeFilter, Guid? departmentId, Guid? positionId);
    }
}
