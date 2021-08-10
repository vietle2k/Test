using Dapper;
using MISA.Core;
using MISA.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class EmployeeRepository :DbContext<Employee>, IEmployeeContext
    {
        /// <summary>
        /// Hàm lấy newEmployeeCode
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public string GetNewEmployeeCode()
        {
            // Câu truy vấn
            var sqlCommand = $"Proc_GetNewEmployeeCode";
            // Gửi truy vấn lên db và nhận lại kết quả
            var newEmployeeCode = DbConnection.Query<string>(sqlCommand, commandType: CommandType.StoredProcedure);
            // Trả về kết quả
            if (newEmployeeCode != null)
            {
                return newEmployeeCode.First().ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Hàm lấy data phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public object GetEmployeesFilterPaging(int? pageSize, int? pageIndex, string employeeFilter, Guid? departmentId, Guid? positionId)
        {
            // Câu lệnh truy vấn
            var sqlCommand = $"Proc_GetEmployeeFilterPaging";
            // Tạo params truyền vào procedure
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeFilter", employeeFilter, direction: ParameterDirection.Input);
            parameters.Add("DepartmentId", departmentId, direction: ParameterDirection.Input);
            parameters.Add("PositionId", positionId, direction: ParameterDirection.Input);
            parameters.Add("PageOffset", pageSize * pageIndex, direction: ParameterDirection.Input);
            parameters.Add("PageSize", pageSize, direction: ParameterDirection.Input);
            parameters.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employees = DbConnection.Query<Employee>(sqlCommand, parameters, commandType: CommandType.StoredProcedure);
            return new
            {
                TotalPage = parameters.Get<int>("TotalPage"),
                TotalRecord = parameters.Get<int>("TotalRecord"),
                Data = employees
            };
        }


    }
}
