using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Data.models
{

    public enum Category
    {
        Electronics,
        Clothing,
        Food,
        Stationery,
        Other
    }
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public double Price { get; set; }
        public int quaintity { get; set; }
 
        public Category Category { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customers? Customer { get; set; }


    }
}
