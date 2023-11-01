using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Product_Management.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ISIN { get; set; }
        public string Ticker { get; set; }
        public bool FundPrimaryShareClass { get; set; }
        public string FundType { get; set; }
      
        public decimal ClassAssets { get; set; }
  
        public decimal TotalFundAssets { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}