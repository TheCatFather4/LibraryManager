using LibraryManagement.Application;
using LibraryManagement.ConsoleUI.IO;
using LibraryManagement.Core.Interfaces.Application;

namespace LibraryManagement.ConsoleUI
{
    public class App
    {
        IAppConfiguration _config;
        ServiceFactory _serviceFactory;

        public App()
        {
            _config = new AppConfiguration();
            _serviceFactory = new ServiceFactory(_config);
        }

        public void Run()
        {
            do
            {
                int choice = Menus.MainMenu();

                switch (choice)
                {
                    case 1:
                        BorrowerManager();
                        break;
                    case 2:
                        MediaManager();
                        break;
                    case 3:
                        CheckoutManager();
                        break;
                    case 4:
                        return;
                }
            } while (true);
        }

        public void CheckoutManager()
        {
            do
            {
                int choice = Menus.CheckoutMenu();

                switch (choice)
                {
                    case 1:
                        CheckoutWorkflows.CheckoutMedia(_serviceFactory.CreateCheckoutService());
                        break;
                    case 2:
                        CheckoutWorkflows.ReturnMedia(_serviceFactory.CreateCheckoutService());
                        break;
                    case 3:
                        CheckoutWorkflows.GetCheckoutLog(_serviceFactory.CreateCheckoutService());
                        break;
                    case 4:
                        return;
                }
            }
            while (true);
        }

        public void BorrowerManager()
        {
            do
            {
                int choice = Menus.BorrowerMenu();

                switch (choice)
                {
                    case 1:
                        BorrowerWorkflows.GetAllBorrowers(_serviceFactory.CreateBorrowerService());
                        break;
                    case 2:
                        BorrowerWorkflows.GetBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case 3:
                        BorrowerWorkflows.EditBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case 4:
                        BorrowerWorkflows.AddBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case 5:
                        BorrowerWorkflows.DeleteBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case 6:
                        return;
                }
            }
            while (true);
        }

        public void MediaManager()
        {
            do
            {
                int choice = Menus.MediaMenu();

                switch (choice)
                {
                    case 1:
                        MediaWorkflows.GetAllMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 2:
                        MediaWorkflows.AddMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 3:
                        MediaWorkflows.EditMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 4:
                        MediaWorkflows.ArchiveMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 5:
                        MediaWorkflows.ViewArchive(_serviceFactory.CreateMediaService());
                        break;
                    case 6:
                        MediaWorkflows.MostPopularMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 7:
                        return;
                }
            }
            while (true);
        }
    }
}
