using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Product_Management.Models
{
    public class ProductWatchListProducts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductWatchListProductId { get; set; }      

        public Guid ProductId { get; set; }
        public Guid ProductWatchListId { get; set; }

        [ForeignKey("ProductWatchListId")]
        public virtual ProductWatchLists ProductWatchLists { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}