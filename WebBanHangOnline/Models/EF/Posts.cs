using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Posts")]
    public class Posts : CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }

        public string Description { get; set; }
        public string Detail { get; set; }

        public string Image { get; set; }
        public int CategoryId { get; set; }
        public int SeoTitle { get; set; }
        public int SeoDescription { get; set; }
        public int SeoKeywords { get; set; }

        public bool IsActive { get; set; }
        public virtual Category Category { get; set; }

    }
}