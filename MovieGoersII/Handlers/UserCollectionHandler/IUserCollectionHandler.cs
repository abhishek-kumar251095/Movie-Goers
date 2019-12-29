using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers.UserCollectionHandler
{
    public interface IUserCollectionHandler
    {
        public Task<IEnumerable<UserCollection>> GetCollectionByUserIdAsync(int userId);
        public Task<UserCollection> AddCollectionAsync(UserCollection userCollection);
        public Task<bool> CheckCollectionForMovieAsync(int tmdbId, int userId);
        public Task<UserCollection> RemoveCollectionAsync(int tmdbId, int userId);
    }
}
