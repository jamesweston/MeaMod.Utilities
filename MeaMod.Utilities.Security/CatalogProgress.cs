namespace MeaMod.Utilities.Security
{
    public class CatalogProgress(string stage, int total, int processed, string currentItem, string hashAlgorithm)
    {
        public string Stage { get; } = stage;
        public int Total { get; } = total;
        public int Processed { get; } = processed;
        public string CurrentItem { get; } = currentItem;
        public string HashAlgorithm { get; } = hashAlgorithm;
    }
}
