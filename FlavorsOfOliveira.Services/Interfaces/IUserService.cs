using FlavorsOfOliveira.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Services.Interfaces
{
	public interface IUserService
	{
  List<User> GetAll();
		User GetById(int id);
		User Save(User user);
 	void Remove(int id);
  
		bool UpdatePassword(User user, string newPassword);
	}
}
