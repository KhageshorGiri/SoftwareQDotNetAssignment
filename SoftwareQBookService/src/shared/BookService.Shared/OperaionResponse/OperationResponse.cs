
using BookService.Shared.Enums;

namespace BookService.Shared.OperaionResponse;

public class OperationResponse
{
    public OperationResponse() { }

    public OperationResponse(ResponseTypeOption responseType, string message, string data)
        => (ResponseType, Message, Data) = (responseType, message, data);

    public ResponseTypeOption ResponseType { get; set; }
    public string Message { get; set; }
    public string Data { get; set; }

    public static OperationResponse Success(string message = null)
        => new() { ResponseType = ResponseTypeOption.Success, Message = message };

    public static OperationResponse Failure(string message)
        => new() { ResponseType = ResponseTypeOption.Failed, Message = message };

    public static OperationResponse Exception(string message)
        => new() { ResponseType = ResponseTypeOption.Exception, Message = message };

}

public class OperationResponse<T> : OperationResponse
{
    public OperationResponse() { }

    public OperationResponse(ResponseTypeOption responseType, string message, T data)
       => (ResponseType, Message, Data) = (responseType, message, data);

    public new T Data { get; set; }

    public new static OperationResponse<T> Success(string message = null)
        => new() { ResponseType = ResponseTypeOption.Success, Message = message };

    public new static OperationResponse<T> Success(string message, T data)
        => new() { ResponseType = ResponseTypeOption.Success, Message = message, Data = data };

    public new static OperationResponse<T> Failure(string message)
        => new() { ResponseType = ResponseTypeOption.Failed, Message = message };

    public new static OperationResponse<T> Failure(string message, T data)
        => new() { ResponseType = ResponseTypeOption.Failed, Message = message, Data = data };

}

