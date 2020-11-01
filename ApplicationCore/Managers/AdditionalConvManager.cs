using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class AdditionalConvManager : IAdditionalConvManager
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private readonly DbSet<AdditionalConv> _additionalConvs;
        public AdditionalConvManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _additionalConvs = _context.AdditionalConvs;
            _mapper = mapper;
        }
        public async Task<OperationDetails> Create(AdditionalConvDTO additionalConvDTO)
        {
            AdditionalConv check = _additionalConvs.FirstOrDefault(x => x.Name == additionalConvDTO.Name);
            if (check == null)
            {
                AdditionalConv conv = _mapper.Map<AdditionalConvDTO, AdditionalConv>(additionalConvDTO);
                
                await _additionalConvs.AddAsync(conv);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Additional convenience added", "Name");
            }
            return new OperationDetails(false, "Additional convenience with the same name already exists", "Name");
        }
        public void Dispose()
        {

        }
    }
}
