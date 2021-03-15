using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.handlers;

namespace DatingApp.API.models
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParam userParams);
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);
        Task<Like> GetLike(int userid, int recipientId);
    }
}