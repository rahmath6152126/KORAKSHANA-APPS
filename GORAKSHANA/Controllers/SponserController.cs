using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GORAKSHANA.IService;
using GORAKSHANA.Models;
using GORAKSHANA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace GORAKSHANA.Controllers
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
            // _logger
            //     .LogInformation($"Add called - {model.firstName} - {model.lastName}");
            // SponserModel result = null;
            // if (!string.IsNullOrEmpty(model?.Id))
            // {
            //     _logger.LogInformation($"Id - {model.Id}");
            //     result = _service.Get(Builders<SponserModel>.Filter.Eq(nameof(SponserModel.Id), model.Id));
            //     _logger.LogInformation($"Id - {result.code}");
            // }

            // if (result == null)
            //     _service.Create(model);
            // else
            // {
            //     model.code = result.code;
            //     _service.Update(Builders<SponserModel>.Filter.Eq(nameof(SponserModel.Id), model.Id), model);
            // }

            return Ok(_service.Upsert(model));

        }

        [HttpGet("AllMaster")]
        public Dictionary<string, List<string>> AllMaster() => _service.GetDataSourceTypes();

        [HttpPost("AddFile")]
        public string AddFile(IFormFile model)
        {
            return UploadedFile(model);
        }
        private string UploadedFile(IFormFile model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            string uniqueFileName = null;
            string filePath = null;

            if (model.FileName != null)
            {
                string uploadsFolder =
                    Path.Combine(Directory.GetCurrentDirectory(), "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (
                    var fileStream = new FileStream(filePath, FileMode.Create)
                )
                {
                    model.CopyTo(fileStream);
                }
            }
            return filePath;
        }

        [HttpGet("GetList")]
        public IEnumerable<SponserModel> Get() => _service.GetList();

        [HttpGet("GetbyId/{id}")]
        public SponserModel GetbyId(string id)
        {

            return _service.Get(Builders<SponserModel>.Filter.Eq(nameof(SponserModel.Id), id));
        }

        [HttpPost("Search/{reminder}")]
        public List<SearchModel>
        Search([FromBody] SearchModel model, int reminder)
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
                                .Where(x => x.tamilDate <= currentDate)
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
                                evetDate = x.event_date,
                                toSponser = v,
                                event_Type = x.evt_type
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
                                evetDate = x.event_date,
                                toSponser = v,
                                event_Type = x.evt_type
                            };
                        return searchModel;
                    })
                    .ToList();
            }
        }
    }
}
