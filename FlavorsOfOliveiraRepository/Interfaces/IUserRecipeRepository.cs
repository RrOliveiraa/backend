using FlavorsOfOliveira.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Repository.Interfaces
{
	public interface IUserRecipeRepository
	{
		List<UserRecipe> GetAll();
		UserRecipe GetById(int id);
		UserRecipe Add(UserRecipe userrecipe);
		UserRecipe Update(UserRecipe userrecipe);
		void Remove(UserRecipe userrecipe);
	}
}
