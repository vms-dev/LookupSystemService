using System;
using System.Collections.Generic;

namespace LookupSystem.DataAccess.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
