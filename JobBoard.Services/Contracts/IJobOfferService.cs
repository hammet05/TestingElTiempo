using JobBoard.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Services.Contracts
{
    public interface IJobOfferService
    {
        IEnumerable<JobOfferListDto> List(bool onlyActive = true);
        JobOfferListDto Get(int id);
        JobOfferListDto Create(JobOfferCreateDto dto);
        void Delete(int id);              
        void Apply(int offerId, string name, string email);
    }
}
