


using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class Menus
    {
        public static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Library Manager Main Menu");
            Console.WriteLine("=========================");
            Console.WriteLine("1. Borrower Management");
            Console.WriteLine("2. Media Management");
            Console.WriteLine("3. Checkout Management");
            Console.WriteLine("4. Quit\n");

            int choice;

            do
            {
                Console.Write("Enter your choice (1-4): ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= 1 && choice <= 4)
                    {
                        return choice;
                    }                      
                }
                
                Console.WriteLine("Invalid choice!");
            } while (true);
        }

        public static int BorrowerMenu()
        {
            Console.Clear();
            Console.WriteLine("Borrower Management");
            Console.WriteLine("===================");
            Console.WriteLine("1. List all Borrowers");
            Console.WriteLine("2. View a Borrower");
            Console.WriteLine("3. Edit a Borrower");
            Console.WriteLine("4. Add a Borrower");
            Console.WriteLine("5. Delete a Borrower");
            Console.WriteLine("6. Go Back\n");

            int choice;

            do
            {
                Console.Write("Enter choice (1-6): ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= 1 && choice <= 6)
                    {
                        return choice;
                    }
                }

                Console.WriteLine("Invalid choice!");
            } while (true);
        }

        public static int EditBorrowerData()
        {
            Console.Clear();
            Console.WriteLine("Edit Borrower Data");
            Console.WriteLine("==================");
            Console.WriteLine("1. Edit First Name");
            Console.WriteLine("2. Edit Last Name");
            Console.WriteLine("3. Edit Email Address");
            Console.WriteLine("4. Edit Phone Number");
            Console.WriteLine("5. Go Back\n");   

            int choice;

            do
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= 1 && choice <= 5)
                    {
                        return choice;
                    }
                }

                Console.WriteLine("Invalid input!");
            } while (true);

        }

        public static string ConfirmDelete()
        {
            Console.Clear();

            string input;

            do
            {
                Console.Write("Are you sure you would like to delete this borrower? (y/n): ");
                input = Console.ReadLine().ToLower();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This field is required.");
                }
                else if (input == "y" || input == "n")
                {
                    return input;
                }

                Console.WriteLine("Invalid input!");
            }
            while (true);

        }

        public static int MediaMenu()
        {
            Console.Clear();
            Console.WriteLine("Media Management");
            Console.WriteLine("================");
            Console.WriteLine("1. List Media");
            Console.WriteLine("2. Add Media");
            Console.WriteLine("3. Edit Media");
            Console.WriteLine("4. Archive Media");
            Console.WriteLine("5. View Archive");
            Console.WriteLine("6. Most Popular Media Report");
            Console.WriteLine("7. Go Back\n");

            int choice;

            do
            {
                Console.Write("Enter choice (1-7): ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= 1 && choice <= 7)
                    {
                        return choice;
                    }
                }

                Console.WriteLine("Invalid choice!");
            } while (true);
        }

        public static int MediaTypeMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose a Media Type");
            Console.WriteLine("===================");
            Console.WriteLine("1. Book");
            Console.WriteLine("2. DVD");
            Console.WriteLine("3. Digital Audio\n");

            int choice;

            do
            {
                Console.Write("Enter choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice!");
            }
            while (true);
        }

        public static int EditMediaData()
        {
            Console.Clear();
            Console.WriteLine("Edit Media Data");
            Console.WriteLine("==================");
            Console.WriteLine("1. Edit MediaTypeID");
            Console.WriteLine("2. Edit Title");
            Console.WriteLine("3. Go Back\n");

            int choice;

            do
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= 1 && choice <= 3)
                    {
                        return choice;
                    }
                }

                Console.WriteLine("Invalid input!");
            } while (true);
        }

        public static int CheckoutMenu()
        {
            Console.Clear();
            Console.WriteLine("Checkout Management");
            Console.WriteLine("===================");
            Console.WriteLine("1. Checkout");
            Console.WriteLine("2. Return");
            Console.WriteLine("3. Checkout Log");
            Console.WriteLine("4. Go Back\n");

            int choice;

            do
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >=1 && choice <= 4)
                    {
                        return choice;
                    }
                }

                Console.WriteLine("Invalid input!");
            }
            while (true);
        }
    }
}
