﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Position :BaseEntity
    {   
        /// <summary>
        /// id vị trí
        /// </summary>
        public Guid PositionId { get; set; }
        /// <summary>
        /// Mã vị trí
        /// </summary>
        public string PositionCode { get; set; }
        /// <summary>
        /// tên vị trí
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
}
