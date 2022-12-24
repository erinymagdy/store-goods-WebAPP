using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IServices
{
    public interface ICSVService
    {
        public void UploadCSVFile(IFormFile file , string path);
        public IEnumerable<T> ReadCSV<T>(Stream file);
    }
}
