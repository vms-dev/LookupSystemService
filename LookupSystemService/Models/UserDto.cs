using System;
using System.Collections.Generic;

namespace LookupSystemService.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool Fired { get; set; }
        public Guid? ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string SSN { get; set; }
        public string DriverLicense { get; set; }
        public string Email { get; set; }
        public List<string> Activities { get; set; }
    }
}
