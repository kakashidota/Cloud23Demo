using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud23Demo.Models
{
    internal class ShoppingCartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created {  get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool Collected { get; set; }
        
    }

    internal class CreateShoppingCartItem
    {
        public string ItemName { get; set; }
    }


    internal class UpdateShoppingCartItem
    {
        public string ItemName { get; set;}
        public bool Collected { get; set; }
    }
}
