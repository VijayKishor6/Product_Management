using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Product_Management.Models
{
    public class ProductWatchLists
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ProductWatchListId { get; set; }
        [Required]
        public string ListName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CreatedBy { get; set; }
        public  DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        [Required]
        public short Status { get; set; }
    }
}