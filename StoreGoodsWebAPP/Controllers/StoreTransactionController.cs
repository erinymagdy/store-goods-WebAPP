using CsvHelper;
using Domain.Models;
using Domain.ModelsDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StoreGoodsWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreTransactionController : Controller
    {
        private readonly ICSVService _csvService;
        private readonly ILogger<StoreTransactionController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StoreTransactionController(ICSVService csvService ,
            ILogger<StoreTransactionController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _csvService = csvService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("UploadCSVFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UploadCSVFile(IFormFile file)
        {
            if (file != null)
            {
                //Get path of folder you want to save in it (wwwwroor\Resources)
                _logger.LogInformation("Get path of folder you want to save in it");
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "Resources");
                //call services to upload file 
                _logger.LogInformation("Upload CSV file and save it in wwwroot");
                _csvService.UploadCSVFile(file, path);
                _logger.LogInformation("File uploaded");
                var response = new ResponseModel()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "File uploaded"
                };
                return Ok(response);
            }
            else
            {
                _logger.LogError("file is required");
                var response = new ResponseModel()
                {
                    Success = true,
                    StatusCode = 400,
                    Error = "file is required"
                };
                return BadRequest(response);
            }

        }

        [HttpPost("GetStoreTransactionCSV")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStoreTransactionCSV([FromQuery] ApisParameters parameters)
        {
            //Get path of folder you want to save in it (wwwwroor\Resources)
            _logger.LogInformation("Get path of folder you want to save in it");
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Resources");
            //Open uploaded file
            _logger.LogInformation("Open uploaded file");
            FileStream stream = new FileStream(Path.Combine(path, "StoreData"), FileMode.Open);
            //Send this stream to method to Get data in this file
            _logger.LogInformation("Get data in this file");
            var allTransactions = _csvService.ReadCSV<StoreTransaction>(stream);
            //Converting dateTime from (MM/dd/yyyy) formate to (dd/MM/yyyy) formate to can compare it with date in data in file
            _logger.LogInformation("Converting dateTime formate to compare it with date in data in file");
            var startDate = DateTime.Parse(parameters.StartPeriod.ToString("MM/dd/yyyy"));
            var endDate = DateTime.Parse(parameters.EndPeriod.ToString("MM/dd/yyyy"));
            //Filter data by GoodId and date period 
            _logger.LogInformation("Get Transaction of specific good id and withen specific period");
            var TransactionsByPeriod = allTransactions
                    .Where(
                        a => a.GoodID == parameters.GoodID && a.TransactionDate != "" && a.TransactionDate != null
                        && DateTime.Parse(a.TransactionDate).Date >= startDate
                        && DateTime.Parse(a.TransactionDate).Date <= endDate
                    ).ToList();
            var totalAmount = TransactionsByPeriod.Where(a=>a.Amount != null && a.Amount != "").Sum(a=> double.Parse(a.Amount));
            var totalInDirection = TransactionsByPeriod.Where(a => a.Direction == "In").Sum(a => double.Parse(a.Amount));
            var totalOutDirection = TransactionsByPeriod.Where(a => a.Direction == "Out").Sum(a => double.Parse(a.Amount));
            _logger.LogInformation("Transactions retrieved");
            stream.Close();
            var response = new ResponseModelWithData<IEnumerable<StoreTransaction>>
            {
                Success = true,
                StatusCode = 200,
                Message = "Transactions retrieved",
                NumberofTransactions = TransactionsByPeriod.Count,
                TotalAmount = totalAmount,
                RemainingAmount = Math.Abs(totalOutDirection - totalInDirection),
                Data = TransactionsByPeriod
            };
            return Ok(response);
        }
    }
}
