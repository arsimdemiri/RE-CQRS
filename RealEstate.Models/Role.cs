﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class Role : IdentityRole<Guid>
    {
        public string Code { get; set; }
    }
}
