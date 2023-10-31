namespace FakePieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FakePieShopDbContext _fakePieShopDbContext;

        public CategoryRepository(FakePieShopDbContext fakePieShopDbContext)
        {
            _fakePieShopDbContext = fakePieShopDbContext;
        }

        public IEnumerable<Category> AllCategories =>
            // get all categories in alphabetical order
            _fakePieShopDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
