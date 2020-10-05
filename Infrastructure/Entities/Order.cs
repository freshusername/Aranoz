using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsEctive { get; set; }

        public string AppUserId { get; set; }
        public virtual AppUser User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

}
