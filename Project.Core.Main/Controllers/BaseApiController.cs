using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Core.Main.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    [EnableCors]
    public class BaseApiController : ControllerBase
    {
    }
}