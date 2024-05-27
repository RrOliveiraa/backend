using FlavorsOfOliveira.Data.Context;
using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Repository.Implementations
{
	public class UserRecipeRepository : IUserRecipeRepository
	{

		private readonly DbSet<UserRecipe> _dbSet;
		private readonly FlavorsOfOliveiraDBContext _flavorsOfOliveiraDBContext;

		public UserRecipeRepository(FlavorsOfOliveiraDBContext flavorsOfOliveiraDBContext)
		{
			 _dbSet = flavorsOfOliveiraDBContext.Set<UserRecipe>();
    _flavorsOfOliveiraDBContext = flavorsOfOliveiraDBContext;

		}

		public UserRecipe Add(UserRecipe userrecipe)
		{
			 _dbSet.Add(userrecipe);
			 _flavorsOfOliveiraDBContext.SaveChanges();
			 return userrecipe;
		}

		public List<UserRecipe> GetAll()
		{
			 return _dbSet.ToList();
		}

		public UserRecipe GetById(int id)
		{
			 return _dbSet.FirstOrDefault(p => p.Id == id);
		}

		public void Remove(UserRecipe userrecipe)
		{
			 _dbSet.Remove(userrecipe);
			 _flavorsOfOliveiraDBContext.SaveChanges();
		}

		public UserRecipe Update(UserRecipe userrecipe)
		{
			  _dbSet.Update(userrecipe);
			  _flavorsOfOliveiraDBContext.SaveChanges();
			  return userrecipe;
		}
	}
}

