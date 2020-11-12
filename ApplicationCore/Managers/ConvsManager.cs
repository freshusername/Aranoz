using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Managers
{
    public class ConvsManager :  IConvsManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConvsManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationDetails> CreateConv(AdditionalConvDTO convDTO)
        {
            AdditionalConv addConv = _context.AdditionalConvs.FirstOrDefault(p => p.Id == convDTO.Id);
            if (addConv == null)
            {
                addConv = _mapper.Map<AdditionalConvDTO, AdditionalConv>(convDTO);
                await _context.AdditionalConvs.AddAsync(addConv);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Convenience is successfully added","AddConv");
            }
            else
            {
                return new OperationDetails(false, "The convenience is already exist","AddConv");
            }
        }

        public async Task DeleteConv(int Id)
        {
            AdditionalConv addConv = _context.AdditionalConvs.Find(Id);
            _context.Remove(addConv);
            await _context.SaveChangesAsync();
        }

        public async Task<OperationDetails> EditConv(AdditionalConvDTO convDTO)
        {
            AdditionalConv addConv = _context.AdditionalConvs.FirstOrDefault(p => p.Id == convDTO.Id);
            if (addConv.Name != convDTO.Name)
            {
                addConv.Name = convDTO.Name;
                _context.Update(addConv);
                await _context.SaveChangesAsync();
            }
            return new OperationDetails(true, "Convenience is successfully updated", "AddConv");
        }

        public AdditionalConvDTO GetConvById(int Id)
        {
            AdditionalConv addConv = _context.AdditionalConvs.Find(Id);
            return _mapper.Map<AdditionalConv, AdditionalConvDTO>(addConv);
        }

        public List<AdditionalConvDTO> GetConvs()
        {
            List<AdditionalConv> res = _context.AdditionalConvs.ToList();
            return _mapper.Map<List<AdditionalConv>, List<AdditionalConvDTO>>(res);
        }
    }
}
