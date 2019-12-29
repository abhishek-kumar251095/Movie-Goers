using Microsoft.EntityFrameworkCore;
using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGoersIIDAL.Services.Repositories
{
    public class UserCollectionRepository: Repository<UserCollection, ApplicationDBContext>
    {
        public UserCollectionRepository(ApplicationDBContext context): base(context)
        {
        }

        public async Task<UserCollection> SoftDeleteCollectionAsync(UserCollection collection)
        {
            var res = _context.UserCollection.Find(collection.Id);
            res.IsSoftDeleted = true;
            await base.PutAsync(res);
            return res;
        }

        public async Task<IEnumerable<UserCollection>> GetCollectionByUserIdAsync(int userId)
        {
            var collection = await _context.UserCollection.Where(o => o.UserId == userId && !o.IsSoftDeleted).ToListAsync();
            if(!collection.Any())
            {
                return null;
            }
            return collection;
        }

        public async Task<bool> CheckMovieInCollectionAsync(int tmdbId, int userId)
        {
            var res = from t1 in _context.UserCollection
                      join t2 in _context.Movies
                      on t1.MovieId equals t2.Id
                      where (t2.TMDBId == tmdbId) && (t1.UserId == userId) && (!t1.IsSoftDeleted)
                      select new { t2.TMDBId };

            var list = await res.ToListAsync();

            if(res.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserCollection> GetCollectionFromTmdbIdAAsync(int tmdbId, int userId)
        {
            var res = from t1 in _context.UserCollection
                      join t2 in _context.Movies
                      on t1.MovieId equals t2.Id
                      where (t2.TMDBId == tmdbId) && (t1.UserId == userId) && (!t1.IsSoftDeleted)
                      select new UserCollection{ Id = t1.Id, UserId = t1.UserId, User = t1.User, 
                          MovieId = t1.MovieId, Movie = t1.Movie, Rating = t1.Rating, Review = t1.Review, IsSoftDeleted = t1.IsSoftDeleted };

            var collection = await res.FirstOrDefaultAsync();

            return collection;
        }
    }
}
