using Microsoft.AspNetCore.Http;

namespace eStore.ViewModels
{
    public class ProductInfo
    {
        public string name { get; set; }
        public float price { get; set; }
        public IFormFile img1 { get; set; }
        public IFormFile img2 { get; set; }
        public IFormFile img3 { get; set; }

       
        public int Categoryid { get; set; }
    }
}
