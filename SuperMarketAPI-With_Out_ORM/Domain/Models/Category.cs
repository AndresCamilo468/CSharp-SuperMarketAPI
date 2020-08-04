﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();

        public Category(int Id, string Name) {
            this.Id = Id;
            this.Name = Name;
            Products = new List<Product>();
        }

        public Category(){}
    }
}