using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookupSystem.DataAccess.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public bool Fired { get; set; }

        public Guid? ManagerId { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual UserContact UserContact { get; set; }

    }
}