using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private ICheckoutRepository _checkoutRepository;
        private IBorrowerRepository _borrowerRepository;

        public CheckoutService(ICheckoutRepository checkoutRepository, IBorrowerRepository borrowerRepository)
        {
            _checkoutRepository = checkoutRepository;
            _borrowerRepository = borrowerRepository;
        }

        public Result CheckoutMedia(int mediaID, string email)
        {
            try
            {
                if (_checkoutRepository.IsAvailable(mediaID))
                {
                    var borrower = _borrowerRepository.GetByEmail(email);

                    if (borrower == null)
                    {
                        return ResultFactory.Fail("Borrower not found!");
                    }
                    else if (borrower.CheckoutLogs.Where(cl => cl.ReturnDate == null).Count() >= 3)
                    {
                        return ResultFactory.Fail("This borrower has reached their checkout limit of 3!");
                    }
                    else if (borrower.CheckoutLogs.Where(cl => cl.DueDate < DateTime.Today && cl.ReturnDate == null).Any())
                    {
                        return ResultFactory.Fail("This borrower has overdue items and cannot checkout any more!");
                    }
                    else
                    {
                        var checkoutLog = new CheckoutLog
                        {
                            BorrowerID = borrower.BorrowerID,
                            MediaID = mediaID,
                            CheckoutDate = DateTime.Today,
                            DueDate = DateTime.Today.AddDays(7)
                        };

                        _checkoutRepository.Checkout(checkoutLog);
                        return ResultFactory.Success();
                    }
                }
                else
                {
                    return ResultFactory.Fail("That item is not available for checkout!");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<Media>> GetAvailableMedia()
        {
            try
            {
                var medias = _checkoutRepository.GetMedias();
                return ResultFactory.Success(medias);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<CheckoutLog>> GetCheckoutLog()
        {
            try
            {
                var checkoutLog = _checkoutRepository.Log();
                return ResultFactory.Success(checkoutLog);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckoutLog>>(ex.Message);
            }
        }

        public Result<List<CheckoutLog>> GetBorrowerLogs(string email)
        {
            try
            {
                var borrower = _borrowerRepository.GetByEmail(email);
                var logList = _checkoutRepository.GetBorrowerLogs(borrower.BorrowerID);
                return ResultFactory.Success(logList);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckoutLog>>(ex.Message);
            }
        }

        public Result ReturnItem(CheckoutLog log)
        {
            try
            {
                _checkoutRepository.Return(log);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }
    }
}
