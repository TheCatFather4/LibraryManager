using Azure.Core;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class CheckoutWorkflows
    {
        public static void GetCheckoutLog(ICheckoutService checkoutService)
        {
            Console.Clear();
            Console.WriteLine("Checked Out Media List");
            Console.WriteLine("======================");
            Console.WriteLine($"{"Name", -8} {"", -10} {"Email", -25} {"Title", -27} {"Rented", -12} {"Due"}");
            Console.WriteLine(new string('=', 100));
            var result = checkoutService.GetCheckoutLog();

            if (result.Ok)
            {
                if (result.Data.Count() == 0)
                {
                    Console.WriteLine("There are no checked out items.");
                }
                else
                {
                    foreach (var l in result.Data)
                    {
                        Console.Write($"{l.Borrower.FirstName,-8} {l.Borrower.LastName,-10} {l.Borrower.Email,-25} {l.Media.Title,-27} {l.CheckoutDate.ToShortDateString(),-12} ");

                        if (DateTime.Today > l.DueDate)
                        {                            
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{l.DueDate.ToShortDateString()}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"{l.DueDate.ToShortDateString()}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void CheckoutMedia(ICheckoutService checkoutService)
        {
            Console.Clear();

            var email = Utilities.GetRequiredString("Enter borrower email: ");

            do
            {
                Console.WriteLine("Available Medias\n");
                Console.WriteLine($"{"MediaID",-10} {"Title",-32}");
                Console.WriteLine(new string('=', 50));

                var result = checkoutService.GetAvailableMedia();

                if (result.Ok)
                {
                    foreach (var m in result.Data)
                    {
                        Console.WriteLine($"{m.MediaID,-10} {m.Title,-32}");
                    }
                    Console.WriteLine();

                    var selected = Utilities.GetMediaByID(result.Data);

                    var checkoutResult = checkoutService.CheckoutMedia(selected.MediaID, email);

                    if (checkoutResult.Ok)
                    {
                        Console.WriteLine($"{selected.Title} has been checked out.");

                        if (!Utilities.YesOrNo("Would you like to borrow another title? "))
                        {
                            return;
                        }
                        Console.Clear();

                    }
                    else
                    {
                        Console.WriteLine(checkoutResult.Message);
                        Utilities.AnyKey();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine(result.Message);
                    Utilities.AnyKey();
                    return;
                }
            }
            while (true);
        }

        public static void ReturnMedia(ICheckoutService checkoutService)
        {
            Console.Clear();

            var email = Utilities.GetRequiredString("Enter borrower email: ");

            var result = checkoutService.GetBorrowerLogs(email);

            List<CheckoutLog> userLogs = new List<CheckoutLog>();

            if (result.Ok)
            {
                if (result.Data.Count() == 0)
                {
                    Console.WriteLine("Borrower has no checked out items!");
                    Utilities.AnyKey();
                    return;
                }
                else
                {
                    Console.WriteLine("\nBorrower's checked out items");
                    Console.WriteLine("============================");
                    Console.WriteLine($"{"ID",-5} {"Title",-40}");
                    Console.WriteLine(new string('=', 50));

                    foreach (var l in result.Data)
                    {
                        Console.WriteLine($"{l.CheckoutLogID, -5} {l.Media.Title, -40}");
                        userLogs.Add(l);
                    }
                }
                Console.WriteLine();

                int choice = Utilities.GetPositiveInteger("Enter the ID of the item you'd like to return: ");

                foreach (var log in userLogs)
                {
                    if (log.CheckoutLogID == choice)
                    {
                        log.ReturnDate = DateTime.Today;

                        var returnResult = checkoutService.ReturnItem(log);

                        if (returnResult.Ok)
                        {
                            Console.WriteLine($"\n{log.Media.Title} has been successfully returned.");
                            Utilities.AnyKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine(returnResult.Message);
                        }
                    }
                }
                Console.WriteLine("ID does not exist for user!");
                Utilities.AnyKey();
            }
            else
            {
                Console.WriteLine(result.Message);
                Utilities.AnyKey();
            }
        }
    }
}
