using System;

namespace LookupSystemService.Models
{
    public class ShortUserInfo
    {
        public bool Fired { get; set; }
        public Guid? ManagerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
