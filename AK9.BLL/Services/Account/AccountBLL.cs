using AK9.AppHelper.Models;
using AK9.DAL.EntityModel.Entities;
using AK9.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AK9.BLL.Services.Account
{
    public class AccountBLL : IAccountBLL
    {
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<User> _signInManager;
        private readonly AspNetUserManager<User> _userManager;

        public AccountBLL(AspNetUserManager<User> userManager,
            SignInManager<User> signInManager,
            IUnitOfWork uow)
        {
            _uow = uow;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public SignInModel SignIn(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public SignInModel SignIn(SignInModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
