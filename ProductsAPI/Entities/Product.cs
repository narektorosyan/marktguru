﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required] 
        public bool Available { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        [Timestamp]
        [Required] 
        public byte[] RowVersion { get; set; }
    }
}
