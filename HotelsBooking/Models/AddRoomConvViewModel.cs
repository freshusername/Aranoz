using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class AddRoomConvViewModel
    {
        public IEnumerable<HotelRoomConvsViewModel> convs { get; set; }

        public List<string> SelectedItems { get; set; }

        public int HotelRoomId { get; set; }

        public int HotelId { get; set; }
    }
}
