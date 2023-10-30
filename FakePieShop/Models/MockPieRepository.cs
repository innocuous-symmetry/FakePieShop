namespace FakePieShop.Models
{
    public class MockPieRepository : IPieRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return new List<Pie>
                {
                    new Pie
                    {
                        PieId = 1,
                        Name = "Strawberry Pie",
                        Price = 15.95M,
                        ShortDescription = "Lorem Ipsum",
                        LongDescription = "Lorem Ipsum",
                        Category = _categoryRepository.AllCategories.ToList()[0],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg",
                        InStock = true,
                        IsPieOfTheWeek = true,
                        ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg"
                    },
                    new Pie
                    {
                        PieId = 2,
                        Name = "Cheese cake",
                        Price = 18.95M,
                        ShortDescription = "Lorem Ipsum",
                        LongDescription = "Lorem Ipsum",
                        Category = _categoryRepository.AllCategories.ToList()[1],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg",
                        InStock = true,
                        IsPieOfTheWeek = false,
                        ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg"
                    },
                    new Pie
                    {
                        PieId = 3,
                        Name = "Rhubarb Pie",
                        Price = 15.95M,
                        ShortDescription = "Lorem Ipsum",
                        LongDescription = "Lorem Ipsum",
                        Category = _categoryRepository.AllCategories.ToList()[0],
                        ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg",
                        InStock = true,
                        IsPieOfTheWeek = false,
                        ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg"
                    }
                };
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return AllPies.Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return AllPies.FirstOrDefault(p => p.PieId == pieId);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}
