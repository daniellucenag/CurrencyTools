using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Idempotency
{
    public class IdempotencyCacheOptions
    {
        public int IdempotencyCacheTimeMinutes { get; set; }
    }
}
