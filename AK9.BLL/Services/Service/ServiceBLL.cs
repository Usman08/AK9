using AK9.AppHelper.AppConstants;
using AK9.AppHelper.Enums;
using AK9.AppHelper.Models;
using AK9.AppHelper.Utils;
using AK9.DAL.EntityModel.Entities;
using AK9.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.BLL.Services
{
    public class ServiceBLL : BaseBLL<ServiceModel, ServiceSearchModel>, IServiceBLL
    {
        public ServiceBLL(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task<int> DeleteAsync(int Id, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;
            Service service = await _uow.ServiceRepository.GetAsync(Id, cancellationToken);
            string fileToDelete = service.BannerImage;

            if (service != null)
            {
                _uow.ServiceRepository.Delete(service);
                status = await _uow.SaveAsync(cancellationToken);

                if (status > 0)
                {
                    Helper.DeleteFile(FolderName.SERVICE_BANNER_IMAGE_FOLDER, fileToDelete);
                }
            }

            return status;
        }

        public async Task<List<ServiceModel>> GetListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ServiceSearchModel model = new ServiceSearchModel();
            return await PopulateDataAsync(model, cancellationToken);
        }

        public async Task<List<ServiceModel>> GetListAsync(ServiceSearchModel searchModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PopulateDataAsync(searchModel, cancellationToken);
        }

        public async Task<List<ServiceModel>> GetListInsteadOfOneAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            Expression<Func<Service, bool>> filter = i => i.ServiceId != id;

            List<Service> lstService = (await _uow.ServiceRepository.GetAsync(filter: filter, cancellationToken: cancellationToken)).ToList();

            return lstService.Select(row => MapEntityToModel(row)).ToList();
        }

        public async Task<ServiceModel> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            ServiceModel model = null;
            Service service = (await _uow.ServiceRepository.GetAsync(id, cancellationToken));

            if (service != null)
            {
                model = MapEntityToModel(service);
            }

            return model;
        }

        public async Task<int> SaveAsync(ServiceModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;

            if (model != null)
            {
                Service service = new Service
                {
                    ServiceId = model.ServiceId,
                    ServiceName = model.ServiceName,
                    ServiceIconId = model.ServiceIconId,
                    BannerImage = model.BannerImage,
                    DetailedDescription = model.DetailedDescription,
                    Heading = model.Heading,
                    ShortDescription = model.ShortDescription,
                    CreateDate = Helper.GetDate(),
                    CreatedBy = model.LoggedInUserID
                };

                await _uow.ServiceRepository.AddAsync(service, cancellationToken);

                status = await _uow.SaveAsync(cancellationToken);
                model.ServiceId = service.ServiceId;
            }

            return status;
        }

        public async Task<int> UpdateAsync(ServiceModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;

            if (model != null)
            {
                Service service = await _uow.ServiceRepository.GetAsync(model.ServiceId, cancellationToken);

                if (service != null)
                {
                    service.ServiceId = model.ServiceId;
                    service.ServiceName = model.ServiceName;
                    service.ServiceIconId = model.ServiceIconId;
                    service.BannerImage = model.BannerImage;
                    service.DetailedDescription = model.DetailedDescription;
                    service.Heading = model.Heading;
                    service.ShortDescription = model.ShortDescription;

                    status = await _uow.SaveAsync(cancellationToken);
                }
            }

            return status;
        }

        protected async override Task<List<ServiceModel>> PopulateDataAsync(ServiceSearchModel searchModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<ServiceModel> lstProductCategoryModel = new List<ServiceModel>();

            Expression<Func<Service, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchModel.ServiceName))
            {
                filter = i => i.ServiceName.Contains(searchModel.ServiceName);
            }

            Func<IQueryable<Service>, IOrderedQueryable<Service>> orderBy;

            switch (searchModel.SortOrder)
            {
                case SortOrderEnum.Asc:
                    switch (searchModel.SortColumn)
                    {
                        case "ServiceId":
                            orderBy = i => i.OrderBy(k => k.ServiceId);
                            break;
                        case "ServiceName":
                            orderBy = i => i.OrderBy(k => k.ServiceName);
                            break;
                        default:
                            orderBy = i => i.OrderBy(k => k.ServiceId);
                            break;
                    }
                    break;
                default:
                    switch (searchModel.SortColumn)
                    {
                        case "ServiceId":
                            orderBy = i => i.OrderByDescending(k => k.ServiceId);
                            break;
                        case "ServiceName":
                            orderBy = i => i.OrderByDescending(k => k.ServiceName);
                            break;
                        default:
                            orderBy = i => i.OrderByDescending(k => k.ServiceId);
                            break;
                    }
                    break;

            }

            int skipRows = searchModel.PageSize * (searchModel.Page - 1);
            List<Service> lstProductCategory = (await _uow.ServiceRepository.GetAsync(filter: filter, orderBy: orderBy, skip: skipRows, take: searchModel.PageSize, includeProperties: "ServiceIcon", cancellationToken: cancellationToken)).ToList();

            lstProductCategoryModel = lstProductCategory.Select(row => MapEntityToModel(row)).ToList();

            return lstProductCategoryModel;
        }

        public async Task<List<SelectListItem>> PopulateServiceIconList(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _ddlBLL.ServiceIconSelectList(SelectListFirstItemEnum.Select);
        }

        private ServiceModel MapEntityToModel(Service entity)
        {
            return new ServiceModel
            {
                ServiceId = entity.ServiceId,
                ServiceName = entity.ServiceName,
                ServiceIconId = entity.ServiceIconId,
                BannerImage = entity.BannerImage,
                DetailedDescription = entity.DetailedDescription,
                Heading = entity.Heading,
                ShortDescription = entity.ShortDescription,
                ServiceIconName = entity.ServiceIcon?.ServiceIconName
            };
        }
    }
}
