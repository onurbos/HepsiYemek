namespace HepsiYemek.Data.Context
{
    public abstract class DbConfig
    {
        public string DataBaseName { get; set; }
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}