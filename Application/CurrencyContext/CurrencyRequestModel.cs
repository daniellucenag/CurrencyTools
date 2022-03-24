using System.ComponentModel.DataAnnotations;

namespace Application.CurrencyContext
{
    public class CurrencyRequestModel
    {
        [Required(ErrorMessage = "{0} can't be null", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} can't be null", AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} can't be null", AllowEmptyStrings = false)]
        public string CurrencyApiCode { get; set; }
    }
}