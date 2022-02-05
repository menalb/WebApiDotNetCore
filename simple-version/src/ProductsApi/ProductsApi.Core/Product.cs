namespace ProductsApi.Core
{
    public class Product : IEquatable<Product>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EAN { get; set; }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            return Equals(obj as Product);
        }

        public bool Equals(Product? other)
            =>
            other != null &&
            Id == other.Id &&
            Name == other.Name &&
            EAN == other.EAN;

        public override int GetHashCode()
            => HashCode.Combine(Id, Name, EAN);
    }
}