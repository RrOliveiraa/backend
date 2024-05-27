using FlavorsOfOliveira.Data.Context;
using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Repository.Implementations
{
	public class UserRepository : IUserRepository
	{
  private readonly DbSet<User> _dbSet;
		private readonly FlavorsOfOliveiraDBContext _flavorsOfOliveiraDBContext;

  public UserRepository(FlavorsOfOliveiraDBContext flavorsOfOliveiraDBContext)
  {
    _dbSet = flavorsOfOliveiraDBContext.Set<User>();
    _flavorsOfOliveiraDBContext = flavorsOfOliveiraDBContext;
			
	
  }

  

		public User Add(User user)
		 {
			  _dbSet.Add(user);
		   _flavorsOfOliveiraDBContext.SaveChanges();
			  return user;
		 }
 
		 public List<User> GetAll()
		 {
			  return _dbSet.ToList();
		 }

		 public User GetById(int Id)
		 {
			return _dbSet.FirstOrDefault(Users => Users.Id == Id);
		 }

		 public void Remove(User user)
		 {
			  _dbSet.Remove(user);
			  _flavorsOfOliveiraDBContext.SaveChanges();
		 }

		 public User Update(User user)
		 {
			  _dbSet.Update(user);
			  _flavorsOfOliveiraDBContext.SaveChanges();
			  return user;
		 }

		public bool ExistsByUsername(string username)
		{
			return _dbSet.Any(u => u.UserName == username);
		}

		public User GetByUsername(string username)
		{
			return _dbSet.FirstOrDefault(u => u.UserName == username);


		}

		public User GetByEmail(string email)
		{
			return _dbSet.FirstOrDefault(e => e.Email == email);

		}

		public bool AuthenticatedUser(string userName, string password)
		{
			// Consulta o banco de dados para encontrar user com o username fornecido
			var existingUser = _dbSet.FirstOrDefault(u => u.UserName == userName);

			// verifica se o usuário existe e se a senha fornecida corresponde à senha armazenada
			if (existingUser != null && existingUser.Password == password)
			{
				// Credenciais válidas
				return true;
			}
			else
			{
				// Credenciais inválidas
				return false;
			}
		}


	}
}
