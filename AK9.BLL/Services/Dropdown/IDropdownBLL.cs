using AK9.AppHelper.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.BLL.Services
{
    public interface IDropdownBLL
    {
        Task<List<SelectListItem>> ServiceIconSelectList(SelectListFirstItemEnum firstItem = SelectListFirstItemEnum.None, CancellationToken cancellationToken = default(CancellationToken));
    }
}
