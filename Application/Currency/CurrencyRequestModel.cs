using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Currency
{
    public class CurrencyRequestModel
    {
        [Required(ErrorMessage = "{0} can't be null", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} can't be null", AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}