using AK9.AppHelper.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.BLL.Services
{
    public interface IServiceBLL : IBaseBLL<ServiceModel, ServiceSearchModel, int>
    {
        Task<List<SelectListItem>> PopulateServiceIconList(CancellationToken cancellationToken = default(CancellationToken));
        Task<List<ServiceModel>> GetListInsteadOfOneAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
