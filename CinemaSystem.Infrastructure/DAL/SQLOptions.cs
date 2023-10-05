namespace CinemaSystem.Infrastructure.DAL.Repositories
{
    internal class SQLOptions
    {
        public bool UseInMemory { get; set; } = true;
        public string ConnectionString { get; set; } = default!;
    }
}