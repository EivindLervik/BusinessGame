﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessGameServer.Models
{
    public class Property
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
    }
}