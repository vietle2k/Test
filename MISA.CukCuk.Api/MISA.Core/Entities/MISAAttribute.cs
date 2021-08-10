using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{   
    /// <summary>
    /// check trường bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired: Attribute
    {

    }
    /// <summary>
    /// check trường không được phép để trống
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISADisplayName : Attribute
    {
        public string PropertyName = string.Empty;
        public MISADisplayName(string propName)
        {
            this.PropertyName = propName;
        }
    }
    /// <summary>
    /// check trường nhập trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISADuplicate: Attribute
    {

    }
    /// <summary>
    /// check định dạng trường email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISADuplicateEmail : Attribute
    {

    }
}
