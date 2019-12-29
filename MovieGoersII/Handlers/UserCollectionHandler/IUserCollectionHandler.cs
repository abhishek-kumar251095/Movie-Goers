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
    }
}
