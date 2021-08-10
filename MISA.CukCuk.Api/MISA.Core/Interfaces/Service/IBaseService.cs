using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    public interface IBaseService<MISAEntity>
    {   
        /// <summary>
        /// Thêm Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        ServiceResult InsertEntity(MISAEntity entity);
        /// <summary>
        /// Xóa Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult DeleteEntity(Guid entityId);
        /// <summary>
        /// Sửa entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult UpdateEntity(MISAEntity entity);


        //xoa
    }
}
