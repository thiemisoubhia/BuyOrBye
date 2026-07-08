using System.ComponentModel.DataAnnotations;

namespace BuyOrBye.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string Username { get; set; }

    }
}
