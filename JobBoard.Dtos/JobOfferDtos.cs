using System;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Dtos
{
    public class JobOfferListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public string ContractType { get; set; }
        public DateTime PublishedAt { get; set; }
    }


    public class JobOfferCreateDto
    {
        [Required, StringLength(120)]
        public string Title { get; set; }
        [Required, StringLength(4000)]
        public string Description { get; set; }
        [Required, StringLength(120)]
        public string Location { get; set; }
        [Range(0, 100000000)]
        public decimal Salary { get; set; }
        [Required]
        public string ContractType { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class JobOfferUpdateDto : JobOfferCreateDto { }
}
