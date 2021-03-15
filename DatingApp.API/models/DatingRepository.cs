using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Controllers.models;
using DatingApp.API.handlers;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.models
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
            /****/

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Like> GetLike(int userid, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u => u.LikerId == userid && u.LikeeId == recipientId);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p =>p.Photos).FirstOrDefaultAsync(u =>u.id== id);
            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParam userParams)
        {
            var user = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();

            user = user.Where(u => u.id != userParams.Id);

            user = user.Where(u => u.Gender == userParams.Gender);

            if (userParams.Likers)
            {
                var userLikers = await GetUsersLike(userParams.Id, userParams.Likers);
                user = user.Where(u => userLikers.Contains(u.id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUsersLike(userParams.Id, userParams.Likers);
                user = user.Where(u => userLikees.Contains(u.id));
            }

            if(userParams.minAge != 18 || userParams.maxAge != 99)
            {
                var minDoB = DateTime.Today.AddYears(-userParams.maxAge - 1);
                var maxDoB = DateTime.Today.AddYears(-userParams.minAge);
                user = user.Where(u => u.DateOfBirth >= minDoB && u.DateOfBirth <= maxDoB);
            }

            if (!string.IsNullOrEmpty(userParams.orderBy))
            {
                switch (userParams.orderBy)
                {
                    case "created":
                        user = user.OrderByDescending(u => u.Created);
                        break;
                    default:
                        user = user.OrderByDescending(u => u.LastActive);
                        break;
                }
            }


            return await PagedList<User>.CreatAsync(user,userParams.PageNumber,userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUsersLike(int id,bool likes)
        {
            var user = await _context.Users.Include(x => x.Likers).Include(x => x.Likees).FirstOrDefaultAsync(u => u.id == id);
            if (likes)
            {
                return user.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);
            }
            else
            {
                return user.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}