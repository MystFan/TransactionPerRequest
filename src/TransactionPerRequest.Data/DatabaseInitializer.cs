using Microsoft.EntityFrameworkCore;

namespace TransactionPerRequest.Data
{
    public class DatabaseInitializer : IInitializer
    {
        private readonly ApplicationDbContext db;

        public DatabaseInitializer(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Initialize()
        {
            db.Database.Migrate();
            db.SaveChanges();
        }
    }
}