using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Core.Localization;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResults;

namespace HomeManagement.Business.Common.Behaviours;

public class ExceptionBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, Result>
    where TCommand : ICommand
{
    private readonly ILogger<ExceptionBehavior<TCommand, TResponse>> _logger;
    private readonly ILocalizationService _localizationService;

    public ExceptionBehavior(ILogger<ExceptionBehavior<TCommand, TResponse>> logger, ILocalizationService localizationService)
    {
        _logger = logger;
        _localizationService = localizationService;
    }

    public async Task<Result> Handle(TCommand command, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
    {
        try
        {
            var response = await next();

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, _localizationService.GetLocalizedString("ErrorExecutingRequest"), typeof(TCommand).Name);

            return Result.Fail(FailureReasons.GenericError, e);
        }
    }
}
