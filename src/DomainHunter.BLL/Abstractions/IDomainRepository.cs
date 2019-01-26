using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainHunter.BLL
{
    public interface IDomainRepository
    {
        Task<IEnumerable<Domain>> GetAll();
        Task<bool> IsChecked(Domain domain);
        Task Insert(Domain domain);
    }
}
