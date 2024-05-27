using FlavorsOfOliveira.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Services.Interfaces
{
	public interface IUserRecipeService
	{

		List<UserRecipe> GetAll();
		UserRecipe GetById(int id);
		UserRecipe Save(UserRecipe userrecipe);
		void Remove(int id);
	}
}
