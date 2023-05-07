using MediatR;
using OperationResults;

namespace HomeManagement.Business.Common.CommandQuery;

public interface IQuery { }
public interface IQuery<T> : IQuery, IRequest<Result<T>> { }

public interface IQueryHandler<TRequest, TResult> : IRequestHandler<TRequest, Result<TResult>> where TRequest : IQuery<TResult> { }
