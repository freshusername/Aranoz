using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using System;
using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class AdminRoomManager:IAdminRoomManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdminRoomManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OperationDetails> CreateRoom(AdminRoomDTO convDTO)
        {
            Room room = _context.Rooms.FirstOrDefault(p => p.Id == convDTO.Id);
            if (room == null)
            {
                room = _mapper.Map<AdminRoomDTO, Room>(convDTO);
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Room is successfully added", "Room");
            }
            else
            {
                return new OperationDetails(false, "The room is already exist", "Room");
            }
        }

        public async Task DeleteRoom(int Id)
        {
            Room room = _context.Rooms.Find(Id);
            _context.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<OperationDetails> EditRoom(AdminRoomDTO convDTO)
        {
            Room room = _context.Rooms.FirstOrDefault(p => p.Id == convDTO.Id);
            if (room.RoomType != convDTO.RoomType)
            {
                room.RoomType = convDTO.RoomType;
                _context.Update(room);
                await _context.SaveChangesAsync();
            }
            return new OperationDetails(true, "Room is successfully updated", "Room");
        }

        public AdminRoomDTO GetRoomById(int Id)
        {
            Room room = _context.Rooms.Find(Id);
            return _mapper.Map<Room, AdminRoomDTO>(room);
        }

        public List<AdminRoomDTO> GetRooms()
        {
            List<Room> res = _context.Rooms.ToList();
            return _mapper.Map<List<Room>, List<AdminRoomDTO>>(res);
        }
    }
}
