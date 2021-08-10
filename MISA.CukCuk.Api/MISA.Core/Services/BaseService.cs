using MISA.Core.Entities;
using MISA.Core.Enum;
using MISA.Core.Interfaces.Infrastructure;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MISA.Core.Enum.MISAEnum;

namespace MISA.Core.Service
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        ServiceResult serviceResult;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseRepository"></param>
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            serviceResult = new ServiceResult();
        }
        /// <summary>
        /// Hàm xóa Entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public ServiceResult DeleteEntity(Guid entityId)
        {   
            
            int rowAffect = _baseRepository.Delete(entityId);
            if (rowAffect > 0)
            {
                serviceResult.Data = new { rowAffect = rowAffect };
                serviceResult.DevMsg = Properties.Resources.DeleteSuccess;
                serviceResult.MisaCode = CodeMISAEnum.Success;
            }
            else
            {
                serviceResult.Success = false;
            }
            return serviceResult;
        }
        /// <summary>
        /// hàm thêm Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ServiceResult InsertEntity(MISAEntity entity)
        {
            var isValid = ValidateData(entity);
            if (isValid == true)
            {
                int rowAffect = _baseRepository.Insert(entity);
                if (rowAffect > 0)
                {
                    serviceResult.Data = new { rowAffect = rowAffect }; //_baseRepository.Insert(entity);
                    serviceResult.DevMsg = Properties.Resources.InsertSuccess;
                    serviceResult.MisaCode = CodeMISAEnum.Success;
                }
                else
                {
                    serviceResult.Success = false;
                }
               
            }
            else
            {
                serviceResult.MisaCode = CodeMISAEnum.NotValid;
            }
            return serviceResult;
        }
        /// <summary>
        /// Hàm Sửa Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ServiceResult UpdateEntity(MISAEntity entity)
        {
            int rowAffect = _baseRepository.Update(entity);
            if (rowAffect > 0)
            {
                serviceResult.Data = new { rowAffect = rowAffect }; //_baseRepository.Update(entity);
                serviceResult.DevMsg = Properties.Resources.UpdateSuccess;
                serviceResult.MisaCode = CodeMISAEnum.Success;
            }
            else
            {
                serviceResult.Success = false;
            }
            return serviceResult;
        }
        /// <summary>
        /// hàm lấy Property
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        private object GetPropertyAttributes(PropertyInfo prop, string attributeName)
        {
           
            foreach (CustomAttributeData attribData in prop.GetCustomAttributesData())
            {
                string typeName = attribData.Constructor.DeclaringType.Name; // Lấy tên của custom attribute
                if (attribData.ConstructorArguments.Count == 1 && // Kiểm tra custom attribute có giá trị không
                    (typeName == attributeName || typeName == attributeName + "Attribute")) // So sánh với attribute cần tìm
                {
                    return attribData.ConstructorArguments[0].Value;
                }
            }
            return null;
        }
        private bool ValidateData(MISAEntity entity)
        {
            var isValid = true;
            var mesError = new List<String>();
            //Validate chung
            var properties = entity.GetType().GetProperties();
            
            foreach (var property in properties)
            {
                PropertyInfo prop = typeof(MISAEntity).GetProperty(property.Name);
                var displayName = GetPropertyAttributes(prop, "MISADisplayName");
                //1.Các thông tin bắt buộc nhập:

                if (property.IsDefined(typeof(MISARequired), false)){
                    var propertyValue = property.GetValue(entity);
                    //var displayName = property.GetCustomAttributes(typeof(MISADisplayName), true);
                    if(string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        isValid = false;
                        mesError.Add($"Thông tin {displayName} không được phép để trống");
                        serviceResult.Success = false;
                        //serviceResult.DevMsg = $"Thông tin {displayName} không được phép để trống";
                        serviceResult.MisaCode = CodeMISAEnum.NotValid;
                    }
                }
                //2.Các thông tin nhập trùng:

                if (property.IsDefined(typeof(MISADuplicate), false))
                {
                    var propertyValue = property.GetValue(entity);
                    //var displayName = property.GetCustomAttributes(typeof(MISADisplayName), true);
                    var propName = property.Name;
                    var isDuplication = _baseRepository.checkDuplicate(propertyValue.ToString(), propName);
                    if(isDuplication == true)
                    {
                        isValid = false;
                        mesError.Add($"Thông tin {displayName} đã tồn tại");
                        serviceResult.Success = false;
                        //serviceResult.DevMsg = $"Thông tin {displayName} đã tồn tại";
                        serviceResult.MisaCode = CodeMISAEnum.NotValid;
                    }
                }
                //3.Validate Email:

                if (property.IsDefined(typeof(MISADuplicateEmail), false))
                {
                    var propertyValue = property.GetValue(entity);
                    var propName = property.Name;
                    var emailTemplate = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                    if (!Regex.IsMatch(propertyValue.ToString(), emailTemplate))
                    {
                        isValid = false;
                        mesError.Add($"Thông tin {displayName} đã nhập sai định dạng");
                        serviceResult.Success = false;
                        //serviceResult.DevMsg = $"Thông tin {displayName} đã tồn tại";
                        serviceResult.MisaCode = CodeMISAEnum.NotValid;
                    }
                }
                
            }
            serviceResult.Data = mesError;
            //Validate rieng
            //if(isValid == true)
            //{
                
            //}
            return isValid;
        }
    }
}
