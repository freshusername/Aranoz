using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IConvsManager
    {
        List<AdditionalConvDTO> GetConvs();
        AdditionalConvDTO GetConvById(int Id);
        Task<OperationDetails> CreateConv(AdditionalConvDTO convDTO);
        Task<OperationDetails> EditConv(AdditionalConvDTO convDTO);
        Task DeleteConv(int Id);
    }
}
