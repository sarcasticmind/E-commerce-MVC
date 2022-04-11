using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore.Models
{
    public class Product
    {
        public int id { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image1 { get; set; }
        [Required]
        public string Image2 { get; set; }
        [Required]
        public string Image3 { get; set; }

       

        [Required]
        public float Price { get; set; }


        [ForeignKey("Cat")]
        public int Cat_id { set; get; }
        public virtual Category Cat { set; get; }
    }
}
