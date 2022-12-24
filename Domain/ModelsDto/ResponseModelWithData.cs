using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class ResponseModelWithData<T> : ResponseModel
    {
        public ResponseModelWithData()
        {

        }
        public ResponseModelWithData(T data)
        {
            this.Data = data;
        }
        public T Data { get; set; }
        public int NumberofTransactions { get; set; }
        public double TotalAmount { get; set; }
        public double RemainingAmount { get; set; }
    }
}
