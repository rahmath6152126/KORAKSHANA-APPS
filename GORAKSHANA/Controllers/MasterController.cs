using System;
using System.Collections.Generic;
using System.Linq;
using GORAKSHANA.IService;
using GORAKSHANA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GORAKSHANA.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class MasterController : ControllerBase
    {
        private readonly ILogger<MasterController> _logger;
        private readonly IMasterService _service;
        public MasterController(IMasterService service)
        {
            this._service = service;
        }

        [HttpPost("Add/{command}")]
        public ActionResult Add([FromBody] MasterModel model,Common command)
        {
            if (!TryValidateModel(model, nameof(model)))
            {
                throw new ArgumentNullException();
            }
                _service.Add(command,model);

            return Ok(true);
        }

        [HttpGet("GetList/{cmd}")]
        public IEnumerable<MasterModel> Get(Common cmd)
        {
            return _service.GetList(cmd);
        }

        [HttpGet("GetbyId/{id}")]
        public MasterModel GetbyId(string id)
        {
            return _service.Get(id);
        }




    }
}
