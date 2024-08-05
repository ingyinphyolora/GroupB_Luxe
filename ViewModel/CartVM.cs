namespace INFT3050.ViewModel
{
    public class CartVM
    {
        public List<CartItemVM> Items { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return Items?.Sum(item => item.Price * item.Quantity) ?? 0;
            }
        }
    }
}
