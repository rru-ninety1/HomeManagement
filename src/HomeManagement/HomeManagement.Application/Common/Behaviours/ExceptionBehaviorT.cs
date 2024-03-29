﻿using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Core.Localization;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResults;

namespace HomeManagement.Business.Common.Behaviours;

public class ExceptionBehaviorT<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IQuery
    where TResponse : class
{
    private readonly ILogger<ExceptionBehaviorT<TRequest, TResponse>> _logger;
    private readonly ILocalizationService _localizationService;

    public ExceptionBehaviorT(ILogger<ExceptionBehaviorT<TRequest, TResponse>> logger, ILocalizationService localizationService)
    {
        _logger = logger;
        _localizationService = localizationService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var response = await next();

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, _localizationService.GetLocalizedString("ErrorExecutingRequest"), typeof(TRequest).Name);

            var responseType = typeof(TResponse);

            if (responseType.IsGenericType)
            {
                var innerType = responseType.GetGenericArguments()[0];
                var resultType = typeof(Result<>).MakeGenericType(innerType);
                return resultType.GetMethod("Fail", new[] { typeof(int), typeof(Exception), typeof(ValidationError) }).Invoke(null, new object[] { FailureReasons.GenericError, e, null }) as TResponse;
            }

            return Result.Fail(FailureReasons.GenericError, e) as TResponse;
        }
    }
}
