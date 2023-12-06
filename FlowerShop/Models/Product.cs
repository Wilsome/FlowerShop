using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Models
{
    public partial class Product
    {
        public Product()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Name must be between 3-50 characters", MinimumLength = 3)]
        public string Name { get; set; } = null!;
        [StringLength(200,ErrorMessage ="Description cannot be longer than 200 characters.")]
        public string? Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        public bool Deleted { get; set; }

        //public string? Image { get; set; } = string.Empty;

        public virtual ProductType ProductType { get; set; } = null!;
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
