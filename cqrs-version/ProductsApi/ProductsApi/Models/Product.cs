using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsApi.Models;

public class Product : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public class Warehouse :IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Customer : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }
    public ICollection<Order> Orders { get; set; }= new List<Order>();
}

public class Order : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}