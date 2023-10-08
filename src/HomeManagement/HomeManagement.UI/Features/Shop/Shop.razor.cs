namespace HomeManagement.UI.Features.Shop
{
    public partial class Shop
    {
        private const string SELECTED_TAB_STYLE = "border-b-violet-700 text-violet-700 border-b-4";
        private const string UNSELECTED_TAB_STYLE = "";

        private string ShoppingListStyle = SELECTED_TAB_STYLE;
        private string ChartStyle = UNSELECTED_TAB_STYLE;

        private bool _isShoppingListVisible = true;
        private bool _isChartVisible = false;

        private void ShowShoppingList()
        {
            _isChartVisible = false;
            ChartStyle = UNSELECTED_TAB_STYLE;

            _isShoppingListVisible = true;
            ShoppingListStyle = SELECTED_TAB_STYLE;
        }

        private void ShowChart()
        {
            _isShoppingListVisible = false;
            ShoppingListStyle = UNSELECTED_TAB_STYLE;

            _isChartVisible = true;
            ChartStyle = SELECTED_TAB_STYLE;
        }
    }
}
