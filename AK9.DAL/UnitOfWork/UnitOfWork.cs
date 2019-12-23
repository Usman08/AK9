using AK9.DAL.EntityModel;
using AK9.DAL.Repositories;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AK9Context _context;

        private PolicyRepository _policyRepository;
        private CertificationRepository _certificationRepository;
        private ServiceRepository _serviceRepository;
        private ServiceIconRepository _serviceIconRepository;

        public UnitOfWork(AK9Context context)
        {
            _context = context;
        }

        public PolicyRepository PolicyRepository
        {
            get
            {
                if (_policyRepository == null)
                {
                    _policyRepository = new PolicyRepository(_context);
                }

                return _policyRepository;
            }
        }

        public CertificationRepository CertificationRepository
        {
            get
            {
                if (_certificationRepository == null)
                {
                    _certificationRepository = new CertificationRepository(_context);
                }

                return _certificationRepository;
            }
        }

        public ServiceRepository ServiceRepository
        {
            get
            {
                if (_serviceRepository == null)
                {
                    _serviceRepository = new ServiceRepository(_context);
                }

                return _serviceRepository;
            }
        }

        public ServiceIconRepository ServiceIconRepository
        {
            get
            {
                if (_serviceIconRepository == null)
                {
                    _serviceIconRepository = new ServiceIconRepository(_context);
                }

                return _serviceIconRepository;
            }
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {

                //var outputLines = new List<string>();
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    outputLines.Add(string.Format(
                //        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.UtcNow,
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                //    }
                //}
                //string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\errors.txt";
                //System.IO.File.AppendAllLines(path, outputLines);

                throw e;
            }
        }

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
