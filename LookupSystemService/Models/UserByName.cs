using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LookupSystemService.Models
{
    public class UserByName
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ManagerId { get; set; }
    }
}
