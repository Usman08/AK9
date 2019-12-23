using AK9.AppHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AK9.BLL.Services
{
    public interface IAccountBLL
    {
        SignInModel SignIn(CancellationToken cancellationToken = default(CancellationToken));
        SignInModel SignIn(SignInModel model, CancellationToken cancellationToken = default(CancellationToken));
    }
}
