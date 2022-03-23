using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync<T>(Guid id);
        Task CreateRequestForCommandAsync<T>(Guid id);
    }
}
