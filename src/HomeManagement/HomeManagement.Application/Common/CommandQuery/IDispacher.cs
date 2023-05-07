using MediatR;
using OperationResults;

namespace HomeManagement.Business.Common.CommandQuery;

public interface IDispacher
{
    Task<Result> SendCommand(ICommand command, CancellationToken cancellationToken = default);

    Task<Result<T>> SendQuery<T>(IQuery<T> query, CancellationToken cancellationToken = default);
}

public class MediatRDispacher : IDispacher
{
    private readonly IMediator _mediator;

    public MediatRDispacher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<Result> SendCommand(ICommand command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task<Result<T>> SendQuery<T>(IQuery<T> query, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(query, cancellationToken);
    }
}
