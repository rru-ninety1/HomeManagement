﻿using Fluxor;

namespace HomeManagement.Client.Features.Loader.Store
{
    public static class LoaderReducers
    {
        [ReducerMethod(typeof(LoaderSetProductCategoriesLoadedAction))]
        public static LoaderState OnSetProductCategoriesLoaded(LoaderState state) => state with { ProductCategoriesLoaded = true };

        [ReducerMethod(typeof(LoaderSetProductsLoadedAction))]
        public static LoaderState OnSetProductsLoaded(LoaderState state) => state with { ProductsLoaded = true };
    }
}
