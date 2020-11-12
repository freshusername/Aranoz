using System;
using System.Collections.Generic;
using System.Text;
using static Infrastructure.Enums;

namespace ApplicationCore.DTOs
{
    public class AdminRoomDTO
    {
        public int Id { get; set; }
        public RoomType RoomType { get; set; }
    }
}
