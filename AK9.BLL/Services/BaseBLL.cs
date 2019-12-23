using AK9.AppHelper.Models;
using AK9.DAL.UnitOfWork;
using AK9.BLL.Utils;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace AK9.BLL.Services
{
    public abstract class BaseBLL<T, TSearchModel> where T : BaseModel
    {
        protected readonly LoggedInUserDetail _loggedInUserDetail;
        protected readonly IDropdownBLL _ddlBLL;
        protected readonly IUnitOfWork _uow;

        public BaseBLL(IUnitOfWork uow)
        {
            _uow = uow;
            _loggedInUserDetail = new LoggedInUserDetail();
            _ddlBLL = new DropdownBLL(uow);
        }

        protected BaseModel PopulateLoggedInUserDetail(BaseModel model)
        {
            model.LoggedInUserID = _loggedInUserDetail.LoggedInUserId;

            return model;
        }

        protected abstract Task<List<T>> PopulateDataAsync(TSearchModel model, CancellationToken cancellationToken = default(CancellationToken));
    }
}
