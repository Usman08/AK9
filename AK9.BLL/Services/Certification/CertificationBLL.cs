using AK9.AppHelper.AppConstants;
using AK9.AppHelper.Enums;
using AK9.AppHelper.Models;
using AK9.AppHelper.Utils;
using AK9.DAL.EntityModel.Entities;
using AK9.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.BLL.Services
{
    public class CertificationBLL : BaseBLL<CertificationModel, CertificationSearchModel>, ICertificationBLL
    {
        public CertificationBLL(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task<int> DeleteAsync(int Id, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;
            Certification certification = await _uow.CertificationRepository.GetAsync(Id, cancellationToken);
            string fileToDelete = certification.CertificationImage;

            if (certification != null)
            {
                _uow.CertificationRepository.Delete(certification);
                status = await _uow.SaveAsync(cancellationToken);

                if (status > 0)
                {
                    Helper.DeleteFile(FolderName.SERVICE_BANNER_IMAGE_FOLDER, fileToDelete);
                }
            }

            return status;
        }

        public async Task<List<CertificationModel>> GetListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CertificationSearchModel model = new CertificationSearchModel();
            return await PopulateDataAsync(model, cancellationToken);
        }

        public async Task<List<CertificationModel>> GetListAsync(CertificationSearchModel searchModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PopulateDataAsync(searchModel, cancellationToken);
        }

        public async Task<CertificationModel> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            CertificationModel model = null;
            Certification certification = (await _uow.CertificationRepository.GetAsync(id, cancellationToken));

            if (certification != null)
            {
                model = new CertificationModel
                {
                    CertificationId = certification.CertificationId,
                    CertificationName = certification.CertificationName,
                    CertificationImage = certification.CertificationImage,
                    CertificationUrl = certification.CertificationUrl
                };
            }

            return model;
        }

        public async Task<int> SaveAsync(CertificationModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;

            PopulateLoggedInUserDetail(model);

            Certification certification = new Certification
            {
                CertificationName = model.CertificationName,
                CertificationImage = model.CertificationImage,
                CertificationUrl = model.CertificationUrl,
                CreateDate = Helper.GetDate(),
                CreatedBy = model.LoggedInUserID
            };

            await _uow.CertificationRepository.AddAsync(certification, cancellationToken);

            status = await _uow.SaveAsync(cancellationToken);
            model.CertificationId = certification.CertificationId;

            return status;
        }

        public async Task<int> UpdateAsync(CertificationModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;

            if (model != null)
            {
                Certification certification = await _uow.CertificationRepository.GetAsync(model.CertificationId, cancellationToken);

                if (certification != null)
                {
                    certification.CertificationName = model.CertificationName;
                    certification.CertificationImage = model.CertificationImage;
                    certification.CertificationUrl = model.CertificationUrl;

                    status = await _uow.SaveAsync(cancellationToken);
                }
            }

            return status;
        }

        protected async override Task<List<CertificationModel>> PopulateDataAsync(CertificationSearchModel searchModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<CertificationModel> lstModel = new List<CertificationModel>();

            Expression<Func<Certification, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchModel.CertificationName))
            {
                filter = i => i.CertificationName.Contains(searchModel.CertificationName);
            }

            Func<IQueryable<Certification>, IOrderedQueryable<Certification>> orderBy;

            switch (searchModel.SortOrder)
            {
                case SortOrderEnum.Asc:
                    switch (searchModel.SortColumn)
                    {
                        case "CertificationId":
                            orderBy = i => i.OrderBy(k => k.CertificationId);
                            break;
                        case "CertificationName":
                            orderBy = i => i.OrderBy(k => k.CertificationName);
                            break;
                        default:
                            orderBy = i => i.OrderBy(k => k.CertificationId);
                            break;
                    }
                    break;
                default:
                    switch (searchModel.SortColumn)
                    {
                        case "CertificationId":
                            orderBy = i => i.OrderByDescending(k => k.CertificationId);
                            break;
                        case "CertificationName":
                            orderBy = i => i.OrderByDescending(k => k.CertificationName);
                            break;
                        default:
                            orderBy = i => i.OrderByDescending(k => k.CertificationId);
                            break;
                    }
                    break;

            }

            int skipRows = searchModel.PageSize * (searchModel.Page - 1);
            List<Certification> lstEntity = (await _uow.CertificationRepository.GetAsync(filter: filter, orderBy: orderBy, skip: skipRows, take: searchModel.PageSize, cancellationToken: cancellationToken)).ToList();

            lstModel = lstEntity.Select(row => new CertificationModel
            {
                CertificationId = row.CertificationId,
                CertificationName = row.CertificationName,
                CertificationImage = row.CertificationImage,
                CertificationUrl = row.CertificationUrl
            }).ToList();

            return lstModel;
        }
    }
}
