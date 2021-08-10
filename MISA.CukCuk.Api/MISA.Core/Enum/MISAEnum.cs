using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enum
{
    public class MISAEnum
    {
        public enum CodeMISAEnum
        {
            Success = 100,
            InValid = 200,
            NotValid = 300,
            ErrorSever = 500

        }

        /// <summary>
        /// Giới tính
        /// </summary>
        public enum Gender
        {
            Felmale = 0,
            Male = 1,
            Other = 2
        }
    }
}
