using System.ComponentModel.DataAnnotations;

namespace BuyOrBye.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}
    