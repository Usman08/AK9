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
    public class PolicyBLL : BaseBLL<PolicyModel, PolicySearchModel>, IPolicyBLL
    {
        public PolicyBLL(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task<int> DeleteAsync(int Id, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;
            Policy policy = await _uow.PolicyRepository.GetAsync(Id, cancellationToken);
            string fileToDelete = policy.PolicyFile;

            if (policy != null)
            {
                _uow.PolicyRepository.Delete(policy);
                status = await _uow.SaveAsync(cancellationToken);

                if (status > 0)
                {
                    Helper.DeleteFile(FolderName.SERVICE_BANNER_IMAGE_FOLDER, fileToDelete);
                }
            }

            return status;
        }

        public async Task<List<PolicyModel>> GetListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            PolicySearchModel searchModel = new PolicySearchModel();
            return await PopulateDataAsync(searchModel, cancellationToken);
        }

        public async Task<List<PolicyModel>> GetListAsync(PolicySearchModel searchModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PopulateDataAsync(searchModel, cancellationToken);
        }

        public async Task<PolicyModel> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            PolicyModel model = null;
            Policy policy = (await _uow.PolicyRepository.GetAsync(id, cancellationToken));

            if (policy != null)
            {
                model = new PolicyModel
                {
                    PolicyId = policy.PolicyId,
                    PolicyName = policy.PolicyName,
                    PolicyFile = policy.PolicyFile
                };
            }

            return model;
        }

        public async Task<int> SaveAsync(PolicyModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;

            if (model != null)
            {
                Policy policy = new Policy
                {
                    PolicyName = model.PolicyName,
                    PolicyFile = model.PolicyFile,
                    CreateDate = Helper.GetDate(),
                    CreatedBy = model.LoggedInUserID
                };

                await _uow.PolicyRepository.AddAsync(policy, cancellationToken);

                status = await _uow.SaveAsync(cancellationToken);
                model.PolicyId = policy.PolicyId;
            }

            return status;
        }

        public async Task<int> UpdateAsync(PolicyModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            int status = 0;

            if (model != null)
            {
                Policy policy = await _uow.PolicyRepository.GetAsync(model.PolicyId, cancellationToken);

                if (policy != null)
                {
                    policy.PolicyName = model.PolicyName;
                    policy.PolicyFile = model.PolicyFile;

                    status = await _uow.SaveAsync(cancellationToken);
                }
            }

            return status;
        }

        protected async override Task<List<PolicyModel>> PopulateDataAsync(PolicySearchModel searchModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<PolicyModel> lstProductCategoryModel = new List<PolicyModel>();

            Expression<Func<Policy, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchModel.PolicyName))
            {
                filter = i => i.PolicyName.Contains(searchModel.PolicyName);
            }

            Func<IQueryable<Policy>, IOrderedQueryable<Policy>> orderBy;

            switch (searchModel.SortOrder)
            {
                case SortOrderEnum.Asc:
                    switch (searchModel.SortColumn)
                    {
                        case "PolicyId":
                            orderBy = i => i.OrderBy(k => k.PolicyId);
                            break;
                        case "PolicyName":
                            orderBy = i => i.OrderBy(k => k.PolicyName);
                            break;
                        default:
                            orderBy = i => i.OrderBy(k => k.PolicyId);
                            break;
                    }
                    break;
                default:
                    switch (searchModel.SortColumn)
                    {
                        case "PolicyId":
                            orderBy = i => i.OrderByDescending(k => k.PolicyId);
                            break;
                        case "PolicyName":
                            orderBy = i => i.OrderByDescending(k => k.PolicyName);
                            break;
                        default:
                            orderBy = i => i.OrderByDescending(k => k.PolicyId);
                            break;
                    }
                    break;

            }

            int skipRows = searchModel.PageSize * (searchModel.Page - 1);
            List<Policy> lstProductCategory = (await _uow.PolicyRepository.GetAsync(filter: filter, orderBy: orderBy, skip: skipRows, take: searchModel.PageSize, cancellationToken: cancellationToken)).ToList();

            lstProductCategoryModel = lstProductCategory.Select(row => new PolicyModel
            {
                PolicyId = row.PolicyId,
                PolicyName = row.PolicyName,
                PolicyFile = row.PolicyFile
            }).ToList();

            return lstProductCategoryModel;
        }
    }
}
