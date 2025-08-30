using AutoMapper;
using JobBoard.Common.Enums;
using JobBoard.Domain.Entities;
using JobBoard.Dtos;
using System;

namespace JobBoard.Mapping.Profiles
{
    public class JobOfferProfile : Profile
    {
        public JobOfferProfile()
        {
            CreateMap<JobOffer, JobOfferListDto>()
                .ForMember(d => d.ContractType, o => o.MapFrom(s => ((ContractType)s.ContractType).ToString()));

            CreateMap<JobOfferCreateDto, JobOffer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.PublishedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.ContractType,
                    o => o.MapFrom(src => (int)Enum.Parse(typeof(ContractType), src.ContractType, true)));

            CreateMap<JobOfferUpdateDto, JobOffer>()
                .ForMember(d => d.Id, o => o.Ignore())

                .ForMember(d => d.PublishedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.ContractType, m =>
                {
                    m.MapFrom(s => (ContractType)Enum.Parse(typeof(ContractType), s.ContractType, true));
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
