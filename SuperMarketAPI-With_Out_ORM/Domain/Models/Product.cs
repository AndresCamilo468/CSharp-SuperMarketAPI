using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short QuantityInPackage { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public Product() { }


        public Product(int Id, string Name, short QuantityInPackage, short UnitOfMeasurement, int CategoryId, Category Category){
            this.Id = Id;
            this.Name = Name;
            this.QuantityInPackage = QuantityInPackage;
            this.UnitOfMeasurement = (EUnitOfMeasurement)UnitOfMeasurement;
            this.CategoryId = CategoryId;
            this.Category = Category;
        }
    }
}