﻿using System;

namespace LookupSystemService.Models
{
    public class UserFired
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
    }
}