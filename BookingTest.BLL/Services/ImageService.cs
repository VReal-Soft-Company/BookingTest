using BookingTest.BLL.Services.Base;
using BookingTest.DLL.Database;
using BookingTest.DLL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BookingTest.BLL.Services
{
    public interface IImageService : IBaseService<Images>
    {
        Task SaveImageAsync(Images images,  Stream stream  );
        Task<Stream> GetImageStreamAsync(long roomId, long imageId);
    }
    public class ImageService : BaseService<Images>, IImageService
    {
        public ImageService(ApplicationDataContext contex) : base(contex)
        {
        }

        public async Task SaveImageAsync(Images images, Stream stream)
        {
            var image = await CreateAsync(images);
            string pathToFile = Path.Combine(images.RoomId.ToString(), image.Id.ToString());
            string pathToDirectory = Path.GetDirectoryName(pathToFile);
            if (!Directory.Exists(pathToDirectory)){
                Directory.CreateDirectory(pathToDirectory);
            }
            using (FileStream fs = new FileStream(pathToFile,FileMode.Create, FileAccess.Write)){
              await stream.CopyToAsync(fs);
            }
        }
        public async Task<Stream> GetImageStreamAsync(long roomId, long imageId)
        {
           return new FileStream(Path.Combine(roomId.ToString(), imageId.ToString()), FileMode.Open, FileAccess.Read);
        }
        public override async Task<bool> DeleteAsync(long id)
        {
            var image = await GetById(id);
            File.Delete(Path.Combine(image.RoomId.ToString(), image.Id.ToString()));
            return await base.DeleteAsync(id);
        }
    }
}
