﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMultiplex.Models
{
    public class MultiplexModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

    }
}
