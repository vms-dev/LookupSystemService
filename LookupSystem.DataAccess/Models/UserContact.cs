using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookupSystem.DataAccess.Models
{
    public class UserContact
    {
        public Guid Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeleteDate { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(10)")]
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
