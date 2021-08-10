using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Core.Enum.MISAEnum;

namespace MISA.Core

{
    public class ServiceResult
    {   
        /// <summary>
        ///  Trạng thái Service: true - thành công, false - Có lỗi xảy ra
        /// </summary>
        public bool Success { get; set; } = true;
        public string DevMsg { get; set; } = string.Empty;
        public string UserMsg { get; set; } = string.Empty;

        public object Data { get; set; }

        public CodeMISAEnum MisaCode { get; set; }
    }
}
