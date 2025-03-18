using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IBorrowerService
    {
        Result<List<Borrower>> GetAllBorrowers();
        Result<Borrower> GetBorrower(string email);
        Result AddBorrower(Borrower newBorrower);
        Result UpdateBorrower(Borrower? data);
        Result DeleteBorrower(Borrower? data);
        bool CheckForEmail(string email);
    }
}
