using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EighthWebApplication
{
    public class Product
    {

        public Product(uint id, string name, double price)
        {
            ID = id;
            Name = name;
            Price = price;
        }
        public uint ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
