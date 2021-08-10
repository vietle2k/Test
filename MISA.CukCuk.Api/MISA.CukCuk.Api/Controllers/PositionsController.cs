using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Infrastructure;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{

    public class PositionsController : BaseEntityController<Position>
    {
        public PositionsController(IBaseRepository<Position> baseRepository, IBaseService<Position> baseService) : base(baseRepository, baseService)
        {

        }
    }
}
