using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Product_Management.Models
{
    public class WatchLists
    {
        [Key]
        public int  Id { get; set; }
        public String Name { get; set; }
        public Guid UserId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        public String CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastModified  { get; set; }
        public String LastModifiedBy { get; set; }
        public String Type { get; set; }

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}