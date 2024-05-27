using FlavorsOfOliveira.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Repository.Interfaces
{
	public interface IRecipeRepository
	{
		List<Recipe> GetAll();
		Recipe GetById(int id);
		Recipe Add(Recipe recipe);
		Recipe Update(Recipe recipe);
  Recipe GetByTitle (string title);
		void Remove(Recipe recipe);
  bool ExistsByTitle(string title);
  List<Recipe> GetPendingRecipes();
	}
}
