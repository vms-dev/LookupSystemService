using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LookupSystem.DataAccess.Models
{
    [Index(nameof(Phone), nameof(Email))]
    public class UserContact
    {
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
        
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }


        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
