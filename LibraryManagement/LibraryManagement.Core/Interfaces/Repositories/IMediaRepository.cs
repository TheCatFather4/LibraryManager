using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface IMediaRepository
    {
        void Add(Media media);
        void Edit(Media media);
        List<Media> GetAll();
        List<Media> ViewArchive();
        List<TopMediaItem> GetTopMedia();
    }
}
