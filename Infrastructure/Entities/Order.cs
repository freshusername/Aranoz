using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public bool Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
