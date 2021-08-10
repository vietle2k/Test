using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<MISAEntity>
    {
        List<MISAEntity> GetAll();
        MISAEntity GetById(Guid entityId);
        int Insert(MISAEntity entity);
        int Delete(Guid entityId);
        int Update(MISAEntity entity);
        bool checkDuplicate(string value, string propName);
    }
}
