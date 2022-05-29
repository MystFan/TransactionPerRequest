using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TransactionPerRequest.Data;

// Build config
IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

string connectionString = config.GetConnectionString("DefaultConnection");
var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlServer(connectionString);

var dbContext = new ApplicationDbContext(optionsBuilder.Options);
dbContext.Database.Migrate();
dbContext.SaveChanges();