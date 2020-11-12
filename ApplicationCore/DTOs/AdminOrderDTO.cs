using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Entities;

namespace ApplicationCore.DTOs
{
    public class AdminOrderDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public  AppUser User { get; set; }
    }
}
