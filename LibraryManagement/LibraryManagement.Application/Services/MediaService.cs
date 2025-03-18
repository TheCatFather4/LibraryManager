using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class MediaService : IMediaService
    {
        private IMediaRepository _mediaRepository;
        private ICheckoutRepository _checkoutRepository;

        public MediaService(IMediaRepository mediaRepository, ICheckoutRepository checkoutRepository)
        {
            _mediaRepository = mediaRepository;
            _checkoutRepository = checkoutRepository;
        }

        public Result AddMedia(Media newMedia)
        {
            try
            {
                _mediaRepository.Add(newMedia);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public void EditMedia(Media media)
        {
            _mediaRepository.Edit(media);
        }

        public Result<List<Media>> GetAllMedia()
        {
            try
            {
                var medias = _mediaRepository.GetAll();
                return ResultFactory.Success(medias);

            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<Media>> ViewArchive()
        {
            try
            {
                var medias = _mediaRepository.ViewArchive();
                return ResultFactory.Success(medias);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }
        public Result<List<TopMediaItem>> GetMostPopularMedia()
        {
            try
            {
                var items = _mediaRepository.GetTopMedia();

                return ResultFactory.Success(items.OrderByDescending(i => i.CheckoutCount).Take(3).ToList());
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<TopMediaItem>>(ex.Message);
            }
        }

        public bool IsAvailable(int mediaID)
        {
            if (_checkoutRepository.IsAvailable(mediaID))
            {
                return true;
            }

            return false;
        }

    }
}
