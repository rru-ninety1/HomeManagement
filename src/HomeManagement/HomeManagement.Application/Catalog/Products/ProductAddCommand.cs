using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using HomeManagement.Core.Localization;
using OperationResults;
using Riok.Mapperly.Abstractions;

namespace HomeManagement.Business.Catalog.Products;

public record ProductAddCommand(string Description, string CategoryId) : ICommand;

public class ProductAddCommandHandler : ICommandHandler<ProductAddCommand>
{
    private readonly IDataContext _dataContext;
    private readonly ILocalizationService _localizationService;

    public ProductAddCommandHandler(IDataContext dataContext, ILocalizationService localizationService)
    {
        _dataContext = dataContext;
        _localizationService = localizationService;
    }

    public async Task<Result> Handle(ProductAddCommand command, CancellationToken cancellationToken)
    {
        if (!_dataContext.GetData<ProductCategory>().Any(x => x.Id.Equals(command.CategoryId)))
        {
            return Result.Fail(FailureReasons.ItemNotFound, _localizationService.GetLocalizedString("CategoryNotFound"));
        }

        var product = new ProductAddCommandMapper().ToProduct(command);
        _dataContext.Insert(product);

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}

public sealed class ProductAddCommandValidator : AbstractValidator<ProductAddCommand>
{
    public ProductAddCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage(localizationService.GetLocalizedString("MandatoryDescription"));
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage(localizationService.GetLocalizedString("MandatoryCategory"));
    }
}

[Mapper]
public partial class ProductAddCommandMapper
{
    public partial Product ToProduct(ProductAddCommand comand);
}
