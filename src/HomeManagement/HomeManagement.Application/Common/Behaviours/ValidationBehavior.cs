using FluentValidation;
using FluentValidation.Results;
using HomeManagement.Business.Common.CommandQuery;
using MediatR;
using OperationResults;

namespace HomeManagement.Business.Common.Behaviours;

public class ValidationBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, Result>
     where TCommand : ICommand
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TCommand>> validators) => _validators = validators;

    public async Task<Result> Handle(TCommand request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TCommand>(request);

        var validationResults = new List<ValidationResult>();

        foreach (var validator in _validators)
        {
            validationResults.Add(await validator.ValidateAsync(context));
        }

        var validationErrors = validationResults.SelectMany(x => x.Errors)
            .Where(x => x != null)
            .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
            .Distinct();

        //.GroupBy(
        //    x => x.PropertyName,
        //    x => x.ErrorMessage,
        //    (propertyName, errorMessages) => new
        //    {
        //        Key = propertyName,
        //        Values = errorMessages.Distinct().ToArray()
        //    })
        //.ToDictionary(x => x.Key, x => x.Values);

        if (validationErrors.Any())
        {
            return Result.Fail(FailureReasons.ClientError, validationErrors);
        }

        return await next();
    }
}

public class ValidationBehaviorT<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IQuery
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviorT(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);
        var validationErrors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
            .Distinct();
        //.GroupBy(
        //    x => x.PropertyName,
        //    x => x.ErrorMessage,
        //    (propertyName, errorMessages) => new
        //    {
        //        Key = propertyName,
        //        Values = errorMessages.Distinct().ToArray()
        //    })
        //.ToDictionary(x => x.Key, x => x.Values);

        if (validationErrors.Any())
        {
            var responseType = typeof(TResponse);

            if (responseType.IsGenericType)
            {
                var innerType = responseType.GetGenericArguments()[0];
                var resultType = typeof(Result<>).MakeGenericType(innerType);

                return resultType.GetMethod("Fail", new[] { typeof(int), typeof(IEnumerable<ValidationError>) }).Invoke(null, new object[] { FailureReasons.ClientError, validationErrors }) as TResponse;

            }

            return Result.Fail(FailureReasons.ClientError, validationErrors) as TResponse;
        }

        return await next();
    }
}
