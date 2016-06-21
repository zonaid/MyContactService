using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Redis;
using StackExchange.Redis.Extensions;

namespace MyContactService.Repo.Repositories
{
    public class ContactRepository : Repository<MyContactService.Repo.Models.Contact>
    {
        public ContactRepository(IDatabase database) : base(database)
        {
        }
    }
}