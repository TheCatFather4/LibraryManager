using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Repositories
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private LibraryContext _dbContext;

        public EFCheckoutRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void Checkout(CheckoutLog log)
        {
            _dbContext.CheckoutLog.Add(log);
            _dbContext.SaveChanges();
        }

        public List<CheckoutLog> Log()
        {
            return _dbContext.CheckoutLog
                .Include(cl => cl.Borrower)
                .Include(cl => cl.Media)
                    .ThenInclude(m => m.MediaType)
                .Where(cl => cl.ReturnDate == null)
                .OrderBy(cl => cl.DueDate)
                .ToList();
        }

        public void Return(CheckoutLog log)
        {
            _dbContext.CheckoutLog.Update(log);
            _dbContext.SaveChanges();
        }

        public List<Media> GetMedias()
        {
            return _dbContext.Media.FromSqlInterpolated($@"
                SELECT *
                FROM Media
                WHERE IsArchived = 0 AND MediaID NOT IN
                (SELECT MediaID FROM CheckoutLog WHERE ReturnDate IS NULL)"
                ).ToList();
        }

        public bool IsAvailable(int mediaID)
        {
            return !_dbContext.CheckoutLog
                    .Where(cl => cl.MediaID == mediaID && cl.ReturnDate == null)
                    .Any();
        }

        public List<CheckoutLog> GetBorrowerLogs(int borrowerID)
        {
            return _dbContext.CheckoutLog
                .Include(cl => cl.Media)
                .Where(cl => cl.BorrowerID == borrowerID && cl.ReturnDate == null)
                .ToList();
        }
    }
}
