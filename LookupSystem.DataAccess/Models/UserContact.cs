using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookupSystem.DataAccess.Models
{
    [Index(nameof(Phone), nameof(Email))]
    public class UserContact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? DeleteDate { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string MobilePhone { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Country { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string City { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string SSN { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string DriverLicense { get; set; }
        
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }


        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
