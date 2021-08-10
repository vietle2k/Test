using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core;
using MISA.Core.Interfaces.Infrastructure;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    //[Route("api/v1/employees")]
    //[ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        ResponseError _responseError = new ResponseError();
        IBaseRepository<Employee> _baseRepository;
        IEmployeeService _employeeService;
        public EmployeesController(IBaseRepository<Employee> baseRepository , IEmployeeService employeeService ):base(baseRepository, employeeService)
        {
            _baseRepository = baseRepository;
            _employeeService = employeeService;
        }

        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var res = _employeeService.GetNewEmployeeCode();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResult
                {
                    Data = ex.Message,
                   
                });
            }
        }
    }
}
