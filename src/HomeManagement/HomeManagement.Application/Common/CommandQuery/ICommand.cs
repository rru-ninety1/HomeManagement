using MediatR;
using OperationResults;

namespace HomeManagement.Business.Common.CommandQuery;

public interface ICommand : IRequest<Result> { }

public interface ICommandHandler<T> : IRequestHandler<T, Result> where T : ICommand { }
