
using LibraryManagement.Core.Entities;

namespace LibraryManagement.ConsoleUI.IO
{
    public class Utilities
    {
        public static void AnyKey()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static int GetPositiveInteger(string prompt)
        {
            int result;

            do
            {
                Console.Write(prompt);
                if(int.TryParse(Console.ReadLine(), out result))
                {
                    if(result > 0)
                    {
                        return result;
                    }
                }

                Console.WriteLine("Invalid input, must be a positive integer!");
                AnyKey();
            } while (true);
        }

        public static string GetRequiredString(string prompt)
        {
            string? input;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("Input is required.");
                AnyKey();
            } while (true);
        }

        public static Media GetMediaByID(List<Media> medias)
        {
            int choice;

            do
            {
                Console.Write("Enter the ID of the media you'd like to checkout: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    var media = medias.FirstOrDefault(m => m.MediaID == choice);

                    if (media == null)
                    {
                        Console.WriteLine("That MediaID is not available. Please try another one.");
                    }

                    return media;
                }
                else
                {
                    Console.WriteLine("An ID number is required.");
                }
            }
            while (true);
        }

        public static bool YesOrNo(string prompt)
        {
            string input;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This answer is required.");
                }
                else if (input == "Y")
                {
                    return true;
                }
                else if (input == "N")
                {
                    return false;
                }

                Console.WriteLine("You must select Y or N.");

            } while (true);
        }
    }
}
