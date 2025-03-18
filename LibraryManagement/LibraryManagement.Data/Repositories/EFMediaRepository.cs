using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Repositories
{
    public class EFMediaRepository : IMediaRepository
    {
        private LibraryContext _dbContext;

        public EFMediaRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void Add(Media media)
        {
            _dbContext.Media.Add(media);
            _dbContext.SaveChanges();
        }

        public void Edit(Media media)
        {
            _dbContext.Media.Update(media);
            _dbContext.SaveChanges();
        }

        public List<Media> GetAll()
        {
            return _dbContext.Media.ToList();
        }

        public List<Media> ViewArchive()
        {
            return _dbContext.Media
                .Include(m => m.MediaType)
                .OrderBy(m => m.MediaTypeID)
                .ThenBy(m => m.Title)
                .ToList();
        }

        public List<TopMediaItem> GetTopMedia()
        {
            return _dbContext.CheckoutLog
                .Include(c => c.Media)
                .GroupBy(c => c.Media)
                .Select(group => new TopMediaItem
                {
                    Title = group.Key.Title,
                    CheckoutCount = group.Count()
                })
                .ToList();
        }
    }
}
