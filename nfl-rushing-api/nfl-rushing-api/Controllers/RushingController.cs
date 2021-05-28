using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nfl_rushing_api.Models;
using nfl_rushing_api.Helpers;
using Microsoft.AspNetCore.StaticFiles;

namespace nfl_rushing_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RushingController : ControllerBase
    {
        private readonly ILogger<RushingController> _logger;
        private readonly IRushingService _rushingService;

        public RushingController(ILogger<RushingController> logger, IRushingService rushingService)
        {
            _logger = logger;
            _rushingService = rushingService;
        }

        /// <summary>
        /// Gets table data from provided json file
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] int pageNum, [FromQuery] int pageSize,
                                    [FromQuery] string orderBy, [FromQuery] string filter)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"rushing.json");
            string jsonData = System.IO.File.ReadAllText(path);
            IQueryable<RushingItem> data = (JsonConvert.DeserializeObject<List<RushingItem>>(jsonData)).AsQueryable();

            _rushingService.SearchByName(ref data, filter);

            _rushingService.ApplySort(ref data, orderBy);

            PagedList<RushingItem> Data = PagedList<RushingItem>.ToPagedList(data, pageNum, pageSize);

            // set return data
            var returnData = new
            {
                Data,
                Data.TotalCount,
                Data.PageSize,
                Data.CurrentPage,
                Data.TotalPages,
                Data.HasNext,
                Data.HasPrevious
            };

            return Ok(returnData);
        }

        /// <summary>
        /// Export to CSV using CSVHelper
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("csv")]
        public async Task<IActionResult> GetCsv([FromQuery] string orderBy, [FromQuery] string filter)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"rushing.json");
            string jsonData = System.IO.File.ReadAllText(path);
            IQueryable<RushingItem> data = (JsonConvert.DeserializeObject<List<RushingItem>>(jsonData)).AsQueryable();

            _rushingService.SearchByName(ref data, filter);
            _rushingService.ApplySort(ref data, orderBy);

            var filePath = _rushingService.BuildCsv(data);

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), filePath);
        }

        /// <summary>
        /// Gets content type of file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
