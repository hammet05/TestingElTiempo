using System.ComponentModel.DataAnnotations;
using System;
using JobBoard.Common.Enums;

namespace JobBoard.Domain.Entities
{
    public class JobOffer
    {
        public int Id { get; set; }


        [Required, StringLength(120)]
        public string Title { get; set; }


        [Required, StringLength(4000)]
        public string Description { get; set; }


        [Required, StringLength(120)]
        public string Location { get; set; }


        [Range(0, 100000000)]
        public decimal Salary { get; set; }


        [Required]
        public ContractType ContractType { get; set; }

        public DateTime PublishedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
