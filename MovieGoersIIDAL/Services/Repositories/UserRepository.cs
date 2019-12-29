using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieGoersIIDAL.Services.Repositories
{
    public class UserRepository: Repository<Users, ApplicationDBContext>
    {
        public UserRepository(ApplicationDBContext context): base(context)
        {

        }
    }
}
