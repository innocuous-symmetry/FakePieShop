namespace FakePieShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
