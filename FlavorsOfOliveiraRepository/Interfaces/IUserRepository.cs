using FlavorsOfOliveira.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Repository.Interfaces
{
	public interface IUserRepository
	{
		List<User> GetAll();
		User GetById(int id);
		User Add(User user);
		User Update(User user);
  User GetByUsername(string username);
  User GetByEmail(string email);
		bool ExistsByUsername(string username);
  bool AuthenticatedUser(string userName, string password);
		void Remove(User user);
	}
}
