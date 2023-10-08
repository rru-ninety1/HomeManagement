using Fluxor;
using HomeManagement.Client.Features.Catalog.ProductCategories.Store;
using HomeManagement.Client.Features.Catalog.Products.Store;

namespace HomeManagement.Client.Features.Synchronizer.Store
{
    public class SynchronizerEffects
    {
        [EffectMethod(typeof(SynchronizerStartAction))]
        public async Task StartSync(IDispatcher dispatcher)
        {
            //TODO controllare se c'è la connessione
            dispatcher.Dispatch(new SynchronizerSetOperationDescriptionAction("Check Connessione"));


            dispatcher.Dispatch(new SynchronizerSetOperationDescriptionAction("Sync Categorie"));
            //TODO inviare tutte le categorie nuove
            //TODO leggere tutte le categorie
            //TODO salvare tutte le categorie 
            dispatcher.Dispatch(new ProductCategoryLoadAction());

            dispatcher.Dispatch(new SynchronizerSetOperationDescriptionAction("Sync Prodotti"));
            //TODO inviare tutti i prodotti nuovi
            //TODO leggere tutti i prodotti
            //TODO salvare tutti i prodotti
            dispatcher.Dispatch(new ProductLoadAction());

            dispatcher.Dispatch(new SynchronizerEndAction());
        }
    }
}
