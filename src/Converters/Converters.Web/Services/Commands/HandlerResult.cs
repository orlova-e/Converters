using FluentValidation.Results;

namespace Converters.Web.Services.Commands;

public class HandlerResult<T>
{
    public T Result { get; }
    public OperationStatus Status { get; protected init; }
    public string FailureMessage { get; }
    public IEnumerable<ValidationFailure> ValidationFailures { get; }
    
    protected HandlerResult()
        => Status = OperationStatus.Success;

    public HandlerResult(T result)
    {
        Status = OperationStatus.Success;
        Result = result;
    }

    protected HandlerResult(string message)
    {
        Status = OperationStatus.InternalError;
        FailureMessage = message;
    }
    
    public HandlerResult(IEnumerable<ValidationFailure> failures)
    {
        Status = OperationStatus.ValidationFailure;
        ValidationFailures = failures;
    }
    
    public HandlerResult(OperationStatus status)
    {
        Status = status;
    }

    public static HandlerResult<T> Success()
        => new();
    
    public static HandlerResult<T> Success(T result)
        => new(result);
    
    public static HandlerResult<T> Failure(string message)
        => new(message);
    
    public static HandlerResult<T> Validation(IEnumerable<ValidationFailure> failures)
        => new(failures);

    public static HandlerResult<T> Exception()
        => new()
        {
            Status = OperationStatus.InternalError
        };
}