using FakePieShop.Models;

namespace FakePieShop.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
