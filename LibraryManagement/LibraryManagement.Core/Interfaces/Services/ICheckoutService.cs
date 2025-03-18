using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface ICheckoutService
    {
        Result CheckoutMedia(int mediaID, string email);
        Result<List<Media>> GetAvailableMedia();
        Result<List<CheckoutLog>> GetCheckoutLog();
        Result<List<CheckoutLog>> GetBorrowerLogs(string email);
        Result ReturnItem(CheckoutLog log);
    }
}
