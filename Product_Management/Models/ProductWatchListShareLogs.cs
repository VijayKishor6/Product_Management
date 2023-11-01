using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Product_Management.Models
{
    public class ProductWatchListShareLogs
    {
        [Key]
        public Guid ProductWatchListShareLogId { get; set; }
        public Guid ProductWatchListId { get; set; }
        [Required]
        public string Email { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        [Required]
        public short Status { get; set; }

        [ForeignKey("ProductWatchListId")]
        public virtual ProductWatchLists ProductWatchLists { get; set; }
    }
}