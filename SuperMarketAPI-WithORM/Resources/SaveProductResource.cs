using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Resources
{
    public class SaveProductResource
    {

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int QuantityInPackage { get; set; }

        [Required]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
