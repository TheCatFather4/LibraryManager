using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class MediaWorkflows
    {
        public static void GetAllMedia(IMediaService service)
        {
            int choice = Menus.MediaTypeMenu();

            Console.Clear();
            Console.WriteLine("Media List");
            Console.WriteLine($"{"ID",-5} {"Title",-32} Status");
            Console.WriteLine(new string('=', 70));
            var result = service.GetAllMedia();

            if (result.Ok)
            {
                foreach (var m in result.Data.Where(m => m.MediaTypeID == choice))
                {
                    Console.WriteLine($"{m.MediaID, -5} {m.Title, -32} {ArchiveStatus(m.IsArchived)}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void AddMedia(IMediaService service)
        {
            Media newMedia = new Media();

            newMedia.MediaTypeID = Menus.MediaTypeMenu();
            newMedia.Title = Utilities.GetRequiredString("Enter the media's title (e.g. Rocky III): ");

            var result = service.AddMedia(newMedia);

            if (result.Ok)
            {
                Console.WriteLine($"Media created with id: {newMedia.MediaID}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        private static string ArchiveStatus(bool status)
        {
            if (status == true)
            {
                return "Archived";
            }
            else
            {
                return "Available";
            }
        }

        public static void EditMedia(IMediaService service)
        {
            int choice = Menus.MediaTypeMenu();

            Console.Clear();
            Console.WriteLine("Which media would you like to edit?");
            Console.WriteLine($"{"ID",-5} {"Title",-32}");
            Console.WriteLine(new string('=', 50));

            var result = service.GetAllMedia();

            List<Media> medias = new List<Media>(); 

            if (result.Ok)
            {
                foreach (var m in result.Data.Where(m => m.MediaTypeID == choice && m.IsArchived == false))
                {
                    medias.Add(m);
                    Console.WriteLine($"{m.MediaID,-5} {m.Title,-32}");
                }

                do
                {
                    int idChoice = Utilities.GetPositiveInteger("\nSelect the ID number of the media you would like to edit: ");

                    foreach (var media in medias)
                    {
                        if (media.MediaID == idChoice)
                        {
                            int editChoice = Menus.EditMediaData();

                            switch (editChoice)
                            {
                                case 1:
                                    media.MediaTypeID = Menus.MediaTypeMenu();
                                    service.EditMedia(media);
                                    break;
                                case 2:
                                    media.Title = Utilities.GetRequiredString("Enter new title: ");
                                    service.EditMedia(media);
                                    break;
                                case 3:
                                    return;
                            }
                            Console.WriteLine("Media successfully updated!");
                            Utilities.AnyKey();
                            return;
                        }
                    }

                    Console.WriteLine($"MediaID {idChoice} not found!");
                } while (true);
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();                        
        }

        public static void ArchiveMedia(IMediaService service)
        {
            int choice = Menus.MediaTypeMenu();

            Console.Clear();
            Console.WriteLine("Which media would you like to archive?");
            Console.WriteLine($"{"ID",-5} {"Title",-32}");
            Console.WriteLine(new string('=', 50));

            var result = service.GetAllMedia();

            List<Media> medias = new List<Media>();

            if (result.Ok)
            {
                foreach (var m in result.Data.Where(m => m.MediaTypeID == choice && m.IsArchived == false))
                {
                    medias.Add(m);
                    Console.WriteLine($"{m.MediaID,-5} {m.Title,-32}");
                }

                do
                {
                    int idChoice = Utilities.GetPositiveInteger("\nSelect the ID number of the media you would like to archive: ");

                    foreach (var media in medias)
                    {
                        if (media.MediaID == idChoice && !media.IsArchived)
                        {
                            if (service.IsAvailable(media.MediaID))
                            {
                                media.IsArchived = true;
                                service.EditMedia(media);
                                Console.WriteLine("Media successfully archived!");
                                Utilities.AnyKey();
                                return;
                            }
                            else
                            {
                                Console.WriteLine($"MediaID {idChoice} is already checked out!");
                                Utilities.AnyKey();
                                return;
                            }                            
                        }
                    }

                    Console.WriteLine($"MediaID {idChoice} not found!");
                    Utilities.AnyKey();
                    return;
                } while (true);

            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void ViewArchive(IMediaService service)
        {
            Console.Clear();
            Console.WriteLine("Archived Media\n");
            Console.WriteLine($"{"Title",-32} {"Media Type"}");
            Console.WriteLine(new string('=', 45));
            var result = service.ViewArchive();

            if (result.Ok)
            {
                foreach (var m in result.Data.Where(m => m.IsArchived == true))
                {
                    Console.WriteLine($"{m.Title, -32} {m.MediaType.MediaTypeName}");
                }
            }
            Console.WriteLine(result.Message);
            Utilities.AnyKey();
        }

        public static void MostPopularMedia(IMediaService service)
        {
            Console.Clear();

            var result = service.GetMostPopularMedia();

            if (result.Ok)
            {
                Console.WriteLine("Most Popular Media\n");
                Console.WriteLine($"{"Checkout",-9} Title");

                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{item.CheckoutCount,-9} {item.Title}");
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
