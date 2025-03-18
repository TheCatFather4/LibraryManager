using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IMediaService
    {
        Result AddMedia(Media newMedia);
        void EditMedia(Media media);
        Result<List<Media>> GetAllMedia();
        Result<List<Media>> ViewArchive();
        Result<List<TopMediaItem>> GetMostPopularMedia();
        bool IsAvailable(int mediaID);
    }
}
