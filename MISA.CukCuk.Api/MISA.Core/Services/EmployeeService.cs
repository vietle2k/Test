using MISA.Core.Interfaces.Infrastructure;
using MISA.Core.Interfaces.Service;
using MISA.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Core.Enum.MISAEnum;

namespace MISA.Core.Services
{
    public class EmployeeService :  BaseService<Employee>, IEmployeeService
    {
        public ServiceResult ServiceResult;
        IEmployeeContext _employeeContext;
        public EmployeeService(IEmployeeContext employeeContext):base(employeeContext)
        {
            _employeeContext = employeeContext;
            ServiceResult = new ServiceResult();
        }

        public string GetNewEmployeeCode()
        {
            return _employeeContext.GetNewEmployeeCode();
        }
        public object GetEmployeesFilterPaging(int? pageSize, int? pageIndex, string employeeFilter, Guid? departmentId, Guid? positionId)
        {
            if (employeeFilter == null || pageSize <= 0 || pageIndex < 0)
            {
                var stringError = string.Empty;
                if (employeeFilter == null)
                    stringError += "lọc dữ liệu,";
                if (pageSize <= 0)
                    stringError += "kích cỡ trang,";
                if (pageIndex < 0)
                    stringError += "số trang,";
                stringError.Remove(stringError.Length - 1, 1);

                var serviceResult = new ServiceResult();
                serviceResult.Data = string.Empty;
                serviceResult.DevMsg = string.Format(Properties.Resources.InvalidField, stringError);
                serviceResult.MisaCode = CodeMISAEnum.NotValid;
                return new
                {
                    serviceResult
                };
            }
            else
            {
                return _employeeContext.GetEmployeesFilterPaging(pageSize, pageIndex, employeeFilter, departmentId, positionId);
            }
        }
    }
}
