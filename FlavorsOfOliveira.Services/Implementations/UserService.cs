using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using FlavorsOfOliveira.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlavorsOfOliveira.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;

		}
		public List<User> GetAll()
		{
			return _userRepository.GetAll();
		}

		public User GetById(int id)
		{
			return _userRepository.GetById(id);
		}

		public void Remove(int id)
		{
			User user = _userRepository.GetById(id);
			_userRepository.Remove(user);
		}

		public User Save(User user)
		{
			if (user.Id == 0)
			{
				return _userRepository.Add(user);
			}
			else
			{
				return _userRepository.Update(user);
			}
		}

		public bool UpdatePassword(User user, string newPassword)
		{
			try
			{
				user.Password = newPassword;
				_userRepository.Update(user);
				return true;
			}
			catch (Exception ex)
			{
				
				return false;
			}
  }



	}
}
