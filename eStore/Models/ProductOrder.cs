using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore.Models
{
    public class ProductOrder
    {
        [Key]
        public int id { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public float Price { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public bool isCheckedout { get; set; }

        [ForeignKey("Product")]
        public int Product_id { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("customer")]
        public string Customer_id { get; set; }
        public virtual AccountUser customer { get; set; }
    }
}
