namespace MusicAPI.Database
{
    using Microsoft.EntityFrameworkCore;

    public class MusicAPIContext : DbContext
    {
        public MusicAPIContext(DbContextOptions<MusicAPIContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=HP\\DVISERVER;Database=MusicAPI;password=;Trusted_Connection=True;");
        }
    }
}
