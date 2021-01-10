using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using DBModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Sharedmodels;

namespace KosalaiProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SponserController : ControllerBase
    {
        private readonly ILogger<SponserController> _logger;

        private readonly ISponserServices _service;

        public SponserController(
            ILogger<SponserController> logger,
            ISponserServices sponserService
        )
        {
            _logger = logger;
            _service = sponserService;
        }

        [HttpPost("Add")]
        public ActionResult Add([FromBody] SponserModel model)
        {
            if (!TryValidateModel(model, nameof(model)))
            {
                throw new ArgumentNullException();
            }
            _logger
                .LogInformation($"Add called - {model.firstName} - {model.lastName}");
            SponserModel result = null;
            if (!string.IsNullOrEmpty(model?.Id))
            {
                _logger.LogInformation($"Id - {model.Id}");
                result = _service.Get(model.Id);
                _logger.LogInformation($"Id - {result.code}");
            }

            if (result == null)
                _service.Create(model);
            else
            {
                model.code = result.code;
                _service.Update(model.Id, model);
            }

            return Ok(true);
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string id)
        {
            _service.Remove(id);
            return Ok();
        }

        [HttpGet("GetList")]
        public IEnumerable<SponserModel> Get()
        {
            return _service.GetList();
        }

        [HttpGet("GetbyId/{id}")]
        public SponserModel GetbyId(string id)
        {
            return _service.Get(id);
        }

        [HttpGet("GetMaster")]
        public MasterList GetMaster()
        {
            return new MasterList();
        }

        [HttpPost("Search/{reminder}")]
        public List<SearchModel> Search([FromBody] SearchModel model, int reminder)
        {
            var search = _service.Search(model);

            var currentDate = DateTime.Now.AddDays(15);

            if (reminder == 0)
            {
                return search
                    .Where(x =>
                    {
                        if (x.reminder.Any())
                        {
                            return x
                                .reminder
                                .Where(x => x.eng_date <= currentDate)
                                .Count() >
                            0;
                        }
                        else
                            return false;
                    })
                    .Select(x =>
                    {
                        string v =
                            (
                            sponserType.myself.ToString() == x.sponser_Type
                                ? x.sponser_Type
                                : x.toSponsername
                            );
                        SearchModel searchModel =
                            new SearchModel()
                            {
                                Id = x.Id,
                                name = $"{x.firstName} {x.lastName}",
                                contactno = x.pri_contact_no,
                                sponserType = x.sponser_Type,
                                evetDate = x.event_data.event_date,
                                toSponser = v,
                                event_Type = x.event_data.evt_type
                            };
                        return searchModel;
                    })
                    .ToList();
            }
            else
            {
                return search
                    .Select(x =>
                    {
                        string v =
                            (
                            sponserType.myself.ToString() == x.sponser_Type
                                ? x.sponser_Type
                                : x.toSponsername
                            );
                        SearchModel searchModel =
                            new SearchModel()
                            {
                                Id = x.Id,
                                name = $"{x.firstName} {x.lastName}",
                                contactno = x.pri_contact_no,
                                sponserType = x.sponser_Type,
                                evetDate = x.event_data.event_date,
                                toSponser = v,
                                event_Type = x.event_data.evt_type
                            };
                        return searchModel;
                    })
                    .ToList();
            }
        }
    }
}
