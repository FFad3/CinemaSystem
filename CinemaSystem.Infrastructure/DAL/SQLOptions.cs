using System.Reflection.PortableExecutable;

namespace CinemaSystem.Infrastructure.DAL.Repositories
{
    internal class SQLOptions
    {
        public const string SectionName = "SQLdb";
        public bool UseInMemory { get; set; } = true;
        public string InMemoryDbName { get; set; }
        public string ConnectionString { get; set; } = default!;
    }
}