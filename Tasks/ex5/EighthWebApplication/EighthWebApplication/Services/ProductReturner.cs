using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EighthWebApplication.Services
{
    public class ProductReturner
    {
        private Product[] prods = new Product[]
        {
            new Product(1,"Máslo",24.7),
            new Product(2,"Sůl",45.1),
            new Product(3,"Telecí",465.44),
            new Product(4,"Drůběží",7.78),
            new Product(5,"Bůček",454.5),
            new Product(6,"kuřecí",46.4),
            new Product(7,"Sýr",45)
            
        };
        public ProductReturner()
        { }

        public Product[] GetProducts()
        {

            return prods;
        }

        public Product GetProduct(uint ID)
        {

            return prods[ID];
        }

    }
}
