using APICatalago.Context;

namespace APICatalago.Controllers
{
    public class MyController
    {
        private readonly AppDbContext _context;

        public MyController(AppDbContext context)
        {
            _context = context;
        }
    }
}
