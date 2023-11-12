using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using HomeManagement.Core.Localization;
using Microsoft.EntityFrameworkCore;
using OperationResults;

namespace HomeManagement.Business.Catalog.Products;

public record ProductGetSingleQuery(string Id) : IQuery<Product>;

public class ProductGetSingleQueryHandler : IQueryHandler<ProductGetSingleQuery, Product>
{
    private readonly IReadOnlyDataContext _dataContext;
    private readonly ILocalizationService _localizationService;

    public ProductGetSingleQueryHandler(IReadOnlyDataContext dataContext, ILocalizationService localizationService)
    {
        _dataContext = dataContext;
        _localizationService = localizationService;
    }

    public async Task<Result<Product>> Handle(ProductGetSingleQuery query, CancellationToken cancellationToken)
    {
        var product = await _dataContext.GetData<Product>()
            .FirstOrDefaultAsync(x => x.Id == query.Id)
            .ConfigureAwait(false);

        if (product == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, _localizationService.GetLocalizedString("ProductNotFound"));
        }

        return product;
    }
}

public sealed class ProductGetSingleQueryValidator : AbstractValidator<ProductGetSingleQuery>
{
    public ProductGetSingleQueryValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(localizationService.GetLocalizedString("MandatoryProduct"));
    }
}
