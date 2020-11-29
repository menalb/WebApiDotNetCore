namespace ProductsApi.Product
{
    public record Product : Core.BaseEntity
    {
        public string Name { get; init; }
        public string EAN { get; init; }
    }

}