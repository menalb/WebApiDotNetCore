namespace ProductsApi.Product
{

    public record Product
    {
        public string Name { get; init; }
        public string EAN { get; init; }
        public int Id { get; init; }
    }
}