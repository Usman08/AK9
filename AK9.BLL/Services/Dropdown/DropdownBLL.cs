using AK9.AppHelper.Enums;
using AK9.DAL.EntityModel.Entities;
using AK9.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.BLL.Services
{
    public class DropdownBLL : IDropdownBLL
    {
        private readonly IUnitOfWork _uow;

        public DropdownBLL(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private void AddFirstItem(List<SelectListItem> lst, SelectListFirstItemEnum firstItem = SelectListFirstItemEnum.None)
        {
            switch (firstItem)
            {
                case SelectListFirstItemEnum.Select:
                    AddSelectItem(lst);
                    break;
                case SelectListFirstItemEnum.All:
                    AddAllItem(lst);
                    break;
                default:
                    return;
            }
        }

        private void AddSelectItem(List<SelectListItem> lst)
        {
            lst.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
        }

        private void AddAllItem(List<SelectListItem> lst)
        {
            lst.Insert(0, new SelectListItem { Text = "All", Value = "-1" });
        }

        public async Task<List<SelectListItem>> ServiceIconSelectList(SelectListFirstItemEnum firstItem = SelectListFirstItemEnum.None, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<ServiceIcon> serviceIcons = (await _uow.ServiceIconRepository.GetAllAsync()).ToList();

            listItems.AddRange(serviceIcons.Select(item => new SelectListItem
            {
                Text = item.ServiceIconName,
                Value = item.ServiceIconId.ToString()
            }).ToList<SelectListItem>());

            AddFirstItem(listItems, firstItem);

            return listItems;
        }
    }
}
