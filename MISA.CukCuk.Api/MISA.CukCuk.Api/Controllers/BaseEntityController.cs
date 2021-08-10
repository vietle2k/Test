using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Enum;
using MISA.Core.Interfaces.Infrastructure;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.Core.Enum.MISAEnum;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<MISAEntity> : ControllerBase
    {
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;
        public BaseEntityController(IBaseRepository<MISAEntity> baseRepository, IBaseService<MISAEntity> baseService )
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var entities = _baseRepository.GetAll();
                if (entities != null && entities.Count() == 0)
                {
                     return NoContent();
                }
                else
                {
                     return StatusCode(200, entities);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.ErrorException,
                    errorCode = CodeMISAEnum.ErrorSever
                };
                return StatusCode(500, res);
            }
        }
        [HttpGet("{Id}")]

        public IActionResult GetEntityId(Guid Id)
        {
            //Kết nối với databases:
            try
            {

                //Trả về kết quả:
                // -200 : Ok
                // -201 : Thêm mới thành công dữ liệu vào database
                // -204 : Không có dữ liệu.
                // -400 : Bad request -  dữ liệu đầu vào phía client ko hợp lệ
                // -404 : Không tìm đuộc resource phù hợp
                // -500 : lỗi phía server (code tk lm api cùi)
                var entities = _baseRepository.GetById(Id);

                if (entities != null)
                {
                    return Ok(entities);
                }
                else
                {

                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.ErrorException,
                    errorCode = CodeMISAEnum.ErrorSever
                };
                return StatusCode(500, res);
            }
        }
        [HttpPost]
        public IActionResult Insert(MISAEntity entity)
        {
            try
            {
                var serviceResult = _baseService.InsertEntity(entity);
                if (serviceResult.Success == false)
                {

                    return BadRequest(serviceResult);

                }
                else
                {
                    return StatusCode(201, serviceResult);
                }
            }
            catch(Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.ErrorException,
                    errorCode = CodeMISAEnum.ErrorSever,
                };
                return StatusCode(500, res);
            }
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                //var employeeService = new EmployeeService();
                var serviceResult = _baseService.DeleteEntity(Id);
                if (serviceResult.Success == false)
                {

                    return BadRequest(serviceResult);

                }
                else
                {
                    return StatusCode(200, serviceResult);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.ErrorException,
                    errorCode = CodeMISAEnum.ErrorSever
                };
                return StatusCode(500, res);
            }
        }
        [HttpPut]
        public IActionResult Update(MISAEntity entity)
        {
            try
            {
                //var employeeService = new EmployeeService();
                var serviceResult = _baseService.UpdateEntity(entity);
                if (serviceResult.Success == false)
                {

                    return BadRequest(serviceResult);

                }
                else
                {
                    return StatusCode(200, serviceResult);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resources.ErrorException,
                    errorCode = CodeMISAEnum.ErrorSever
                };
                return StatusCode(500, res);
            }
        }
    }
}
