using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface ICheckoutRepository
    {
        List<CheckoutLog> Log();
        List<Media> GetMedias();
        bool IsAvailable(int mediaID);
        void Checkout(CheckoutLog log);
        void Return(CheckoutLog log);
        List<CheckoutLog> GetBorrowerLogs(int borrowerID);
    }
}
