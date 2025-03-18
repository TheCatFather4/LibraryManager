using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class BorrowerWorkflows
    {
        public static void GetAllBorrowers(IBorrowerService service)
        {
            Console.Clear();
            Console.WriteLine("Borrower List");
            Console.WriteLine($"{"ID",-5} {"Name",-32} Email");
            Console.WriteLine(new string('=', 70));
            var result = service.GetAllBorrowers();

            if (result.Ok)
            {

                foreach (var b in result.Data)
                {
                    Console.WriteLine($"{b.BorrowerID,-5} {b.LastName + ", " + b.FirstName,-32} {b.Email}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void GetBorrower(IBorrowerService service)
        {
            Console.Clear();
            var email = Utilities.GetRequiredString("Enter borrower email: ");
            var result = service.GetBorrower(email);

            if(result.Ok)
            {
                Console.WriteLine("\nBorrower Information");
                Console.WriteLine("====================");
                Console.WriteLine($"Id: {result.Data.BorrowerID}");
                Console.WriteLine($"Name: {result.Data.LastName}, {result.Data.FirstName}");
                Console.WriteLine($"Email: {result.Data.Email}");

                if (result.Data.CheckoutLogs.Any(c => c.ReturnDate == null))
                {
                    Console.WriteLine("\nChecked Out Items");
                    Console.WriteLine(new string('=', 75));

                    Console.WriteLine($"{"Title",-30} {"Type",-15} {"Checkout Date", -15} {"Due Date"}");
                    Console.WriteLine(new string('=', 75));
                    foreach (var item in result.Data.CheckoutLogs.Where(c => c.ReturnDate == null))
                    {
                        Console.WriteLine($"{item.Media.Title, -30} {item.Media.MediaType.MediaTypeName, -15} {item.CheckoutDate.ToShortDateString(), -15} {item.DueDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No items checked out");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void AddBorrower(IBorrowerService service)
        {
            Console.Clear();
            Console.WriteLine("Add New Borrower");
            Console.WriteLine("====================");

            Borrower newBorrower = new Borrower();

            newBorrower.FirstName = Utilities.GetRequiredString("First Name: ");
            newBorrower.LastName = Utilities.GetRequiredString("Last Name: ");
            newBorrower.Email = Utilities.GetRequiredString("Email: ");
            newBorrower.Phone = Utilities.GetRequiredString("Phone: ");

            var result = service.AddBorrower(newBorrower);

            if(result.Ok)
            {
                Console.WriteLine($"Borrower created with id: {newBorrower.BorrowerID}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void EditBorrower(IBorrowerService service)
        {
            Console.Clear();
            var email = Utilities.GetRequiredString("Enter borrower email: ");
            var result = service.GetBorrower(email);

            if (result.Ok)
            {           
                int choice = Menus.EditBorrowerData();

                switch (choice)
                {
                    case 1:
                        result.Data.FirstName = Utilities.GetRequiredString("Enter new First Name: ");
                        break;
                    case 2:
                        result.Data.LastName = Utilities.GetRequiredString("Enter new Last Name: ");
                        break;
                    case 3:
                        string newEmail;

                        do
                        {
                            newEmail = Utilities.GetRequiredString("Enter new Email: ");
                            if (service.CheckForEmail(newEmail))
                            {
                                Console.WriteLine($"{newEmail} already exists for a borrower. Please try a different email!");
                            }
                            else
                            {
                                break;
                            }
                        } while (true);

                        result.Data.Email = newEmail;
                        break;
                    case 4:
                        result.Data.Phone = Utilities.GetRequiredString("Enter new Phone Number: ");
                        break;
                    case 5:
                        return;
                }
                service.UpdateBorrower(result.Data);
                Console.WriteLine("Borrower information successfully updated!");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void DeleteBorrower(IBorrowerService service)
        {
            Console.Clear();
            var email = Utilities.GetRequiredString("Enter borrower email: ");
            var result = service.GetBorrower(email);

            if (result.Ok)
            {
                string choice = Menus.ConfirmDelete();

                if (choice == "y")
                {
                    var deleteResult = service.DeleteBorrower(result.Data);
                    if (deleteResult.Ok)
                    {
                        Console.WriteLine("Borrower successfully deleted!");
                    }
                    else
                    {
                        Console.WriteLine(deleteResult.Message);        
                    }
                }
                else
                {
                    Console.WriteLine("Borrower deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }
    }
}
