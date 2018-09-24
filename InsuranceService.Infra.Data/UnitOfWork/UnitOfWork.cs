using InsuranceServices.Infra.Data.Context;
using System;
using System.Threading.Tasks;

namespace InsuranceServices.Infra.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ISContext _context;
        private bool _disposed;

        public UnitOfWork(ISContext context)
        {
            _context = context;
            _disposed = false;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
