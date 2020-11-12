using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdminRoomManager
    {
        List<AdminRoomDTO> GetRooms();
        AdminRoomDTO GetRoomById(int Id);
        Task<OperationDetails> CreateRoom(AdminRoomDTO convDTO);
        Task<OperationDetails> EditRoom(AdminRoomDTO convDTO);
        Task DeleteRoom(int Id);
    }
}
