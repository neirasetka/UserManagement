﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Core.DTOs
{
    public class AddExpenseDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime ExpirationDate{ get; set; }
    }
}
