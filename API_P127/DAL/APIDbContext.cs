using API_P127.Models;
using Microsoft.EntityFrameworkCore;

namespace API_P127.DAL
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext>opt):base(opt)
        {

        }
        public DbSet<Car>Cars { get; set; }
    }
}
