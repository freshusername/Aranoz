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
        public AdditionalConvManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<AdditionalConvDTO> GetConvs()
        {
            IEnumerable<AdditionalConvDTO> convs = _mapper.Map<IEnumerable<AdditionalConv>, IEnumerable<AdditionalConvDTO>>(_context.AdditionalConvs.ToList());
            return convs;
        }
        public async Task<OperationDetails> Create(AdditionalConvDTO additionalConvDTO)
        {
            AdditionalConv check = _context.AdditionalConvs.FirstOrDefault(x => x.Name == additionalConvDTO.Name);
            if (check == null)
            {
                AdditionalConv conv = _mapper.Map<AdditionalConvDTO, AdditionalConv>(additionalConvDTO);

                await _context.AdditionalConvs.AddAsync(conv);
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
