using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookupSystem.DataAccess.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }
        
        [Column(TypeName = "datetime2")]        
        public DateTime? DeleteDate { get; set; }
        
        public bool Hired { get; set; }
        
        public Guid? ManagerId { get; set; }

        public UserContact UserContact { get; set; }
    }
}
