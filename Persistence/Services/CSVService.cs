using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.ModelsDto;
using Microsoft.AspNetCore.Http;
using Persistence.IServices;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Persistence.Services
{
    public class CSVService : ICSVService
    {
        public void UploadCSVFile(IFormFile file, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, "StoreData"), FileMode.Create))
            {
                file.CopyTo(stream);
                string uploadedFile = fileName;
            }
        }
        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Comment = '#',
                AllowComments = true,
                Delimiter = ";"
            };
            var csv = new CsvReader(reader, csvConfig);
            csv.Context.RegisterClassMap<StoreTransactionMap>();
            var records = csv.GetRecords<T>();
            return records.ToList();
        }
    }
}