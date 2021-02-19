using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LookupSystemService.Models
{
    public class HiredUserViewModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool Hired { get; set; }
        public Guid? ManagerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
    }
}
