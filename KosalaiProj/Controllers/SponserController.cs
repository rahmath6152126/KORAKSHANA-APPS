using System;
using System.Collections.Generic;
using System.Linq;
using GORAKSHANA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GORAKSHANA.IServices;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;

namespace GORAKSHANA.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SponserController : ControllerBase
    {
        private readonly ILogger<SponserController> _logger;

        private readonly ISponserServices<SponserModel> _service;

        public SponserController(
            ILogger<SponserController> logger,
            ISponserServices<SponserModel> sponserService
        )
        {
            _logger = logger;
            _service = sponserService;
        }
        private string GenrateCode()
        {
            var len = _service.GetList().Count() + 1;

            return $"{_service.Codeprefix  }{_service.Appefix + len}";
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
            FilterDefinition<SponserModel> filter;
            if (!string.IsNullOrEmpty(model?.Id))
            {
                _logger.LogInformation($"Id - {model.Id}");
                filter = _service.Builder.Eq(nameof(SponserModel.Id), model.Id);
                result = _service.Get(filter);
                _logger.LogInformation($"Id - {result.code}");
            }
            if (result == null)
            {
                model.code =
                  string.IsNullOrEmpty(model.code) ? GenrateCode() : model.code;
                if (
                _service.GetList(_service.Builder.Eq(nameof(SponserModel.code), model.code))
                        .ToList()
                        .Count() ==
                    0
                )
                {
                    _service.Create(model);
                    return Ok();
                }
                else
                {
                    throw new DuplicateWaitObjectException(nameof(SponserModel.code));
                }
            }

            else
            {
                model.code = result.code;
                var updateFilter = Builders<SponserModel>.Filter.Eq(nameof(SponserModel.Id), model.Id);
                var arrayUpdate = Builders<SponserModel>.Update.Set(nameof(SponserModel.firstName), model.firstName);
                arrayUpdate.Set(nameof(SponserModel.lastName), model.lastName);
                _service.Update(updateFilter, arrayUpdate);


                return Ok(true);
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(string id)
        {
            var filter = Builders<SponserModel>.Filter.Eq(nameof(SponserModel.Id), id);
            _service.Remove(filter, _service.Get(filter));
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
