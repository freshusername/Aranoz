using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class IEnumerableUsersViewModel
    {
        public IEnumerable<UsersViewModel> users { get; set; }
        public AdminPaginationDTO PaginationDTO { get; set; }
    }
}
