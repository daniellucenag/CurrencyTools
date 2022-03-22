using System.ComponentModel.DataAnnotations;

namespace Application
{
    public class CurrencyRequestModel
    {
        [Required(ErrorMessage = "{0} can't be null", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} can't be null", AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}