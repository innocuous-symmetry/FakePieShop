using FakePieShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FakePieShop.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly FakePieShopDbContext _fakePieShopDbContext;

        public PieRepository(FakePieShopDbContext fakePieShopDbContext)
        {
            _fakePieShopDbContext = fakePieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _fakePieShopDbContext.Pies.Include(p => p.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _fakePieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return _fakePieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
