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
            var collection = await _context.UserCollection.Where(o => o.UserId == userId && o.IsSoftDeleted).ToListAsync();
            if(!collection.Any())
            {
                return null;
            }
            return collection;
        }
    }
}
