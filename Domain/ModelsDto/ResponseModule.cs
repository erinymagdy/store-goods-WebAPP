using System;

namespace Domain.ModelsDto
{
    public class ResponseModule<T>
    {
        public ResponseModule()
        {

        }
        public ResponseModule(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
        }

        public bool Success { get; set; }
        public object Error { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int ValidationErrorCode { get; set; }
        public T Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }

    }
}
