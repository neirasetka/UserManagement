﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Core.DTOs
{
    public class AddVehicleDto
    {
        public string Name{ get; set; }
        public string LicensePlate { get; set; }
        public string Manufacturer { get; set; }
    }
}
