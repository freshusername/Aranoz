using static Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace HotelsBooking.Models
{
    public class AdminRoomsViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Room type")]
        public RoomType RoomType { get; set; }
    }
}
