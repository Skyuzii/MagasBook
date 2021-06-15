namespace MagasBook.Application.Common.Dto
{
    public class OperationResultDto
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }

    public class OperationResultDto<T> : OperationResultDto
    {
        public T Data { get; set; }
    }
}