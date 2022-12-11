using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Product")]
    public class Product:CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        public string Alias { get; set; }

        [Required]
        [StringLength(250)]
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }

        public string Image { get; set; }
        public int Price { get; set; }

        public int PriceSale { get; set; }
        public int Quantity { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }

        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public int ProductCategoryId { get; set; }
        public int SeoTitle { get; set; }
        public int SeoDescription { get; set; }
        public int SeoKeywords { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

    }
}