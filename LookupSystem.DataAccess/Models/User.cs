using System;

namespace LookupSystem.DataAccess.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public bool Fired { get; set; }

        public Guid? ManagerId { get; set; }


        public virtual UserContact UserContact { get; set; }
    }
}