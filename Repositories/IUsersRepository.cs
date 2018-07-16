using Chama.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chama.WebApi.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<UserModel> Get();
        UserModel GetById(int id);
        UserModel RegisterOrUpdate(string name, int age);
    }
}
