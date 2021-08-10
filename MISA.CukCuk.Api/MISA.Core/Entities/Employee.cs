
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core

{
    /// <summary>
    /// thông tin nhân viên
    /// </summary>
    /// CreatedBy: LXVIET (22/7/2021)
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính 
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired]
        [MISADisplayName("Mã nhân viên")]
        [MISADuplicate]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [MISARequired]
        [MISADisplayName("Họ và tên")]
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [MISARequired]
        [MISADisplayName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [MISADisplayName("Email")]
        [MISADuplicateEmail]
        public string Email { get; set; }
        /// <summary>
        /// Lương nhân viên
        /// </summary>
        public double? Salary { get; set; }
        [MISARequired]
        [MISADisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [MISARequired]
        [MISADisplayName("Số CMTND")]
        public string IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
        public DateTime? JoinDate { get; set; }
        public int? MartialStatus { get; set; }
        public int? EducationalBackground { get; set; }
        public Guid? QualificationId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? PositionId { get; set; }
        public string PositionName { get; set; }
        public int? WorkStatus { get; set; }
        public string PersonalTaxCode { get; set; }

        #endregion



        #region Other

        #endregion
    }
}
