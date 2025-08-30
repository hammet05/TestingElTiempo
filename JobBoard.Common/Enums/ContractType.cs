using System.ComponentModel.DataAnnotations;

namespace JobBoard.Common.Enums
{
    public enum ContractType
    {
        Undefined = 0,
        [Display(Name = "Tiempo completo")] FullTime = 1,
        [Display(Name = "Medio tiempo")] PartTime = 2,
        [Display(Name = "Temporal")] Temporary = 3,
        [Display(Name = "Pasantía")] Internship = 4,
        [Display(Name = "Contratista")] Contractor = 5
    }
}
