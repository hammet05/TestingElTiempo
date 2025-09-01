using JobBoard.Dtos;
using System.Collections.Generic;

namespace JobBoard.Services.Contracts
{
    public interface IJobOfferService
    {
        IEnumerable<JobOfferListDto> List(bool onlyActive = true);
        JobOfferListDto Get(int id);
        JobOfferListDto Create(JobOfferCreateDto dto);
        void Delete(int id);
        void Apply(int offerId, string name, string email);

        void Update(int id, JobOfferUpdateDto dto);

        void ChangeStatusAsync(int id, bool isActive);
    }
}
