using Microsoft.EntityFrameworkCore;
using ScreenSound.Core.Artistas;
using ScreenSound.Core.Modelos;

namespace ScreenSound.EntityFrameworkCore.Banco
{
    public class ScreenSoundContext: DbContext
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSoundV0;Integrated Security=True;Encrypt=False;";

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies(false);
        }

    }
}
