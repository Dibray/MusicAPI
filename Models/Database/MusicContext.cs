namespace Music.Database
{
    using Microsoft.EntityFrameworkCore;

    using Music.Models;

    public class MusicContext : DbContext
    {
        // User
        public DbSet<Login.Db> Logins { get; set; }
        public DbSet<Password.Db> Passwords { get; set; }
        public DbSet<User.Db> Users { get; set; }
        // User end
        
        public MusicContext() { }

        public MusicContext(DbContextOptions<MusicContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            options.UseSqlServer("Server=HP\\DVISERVER;Database=MusicAPI;password=;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // User
            model.Entity<Login.Db>().HasKey("Value");

            model.Entity<Password.Db>().HasKey("Id");

            model.Entity<User.Db>().HasKey("Id");
            // User end
        }
    }
}
