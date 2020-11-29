namespace ProductsApi.Product
{

    public record Product : BaseEntity
    {
        public string Name { get; init; }
        public string EAN { get; init; }
    }

    public record BaseEntity
    {
        public int Id { get; init; }
    }
}