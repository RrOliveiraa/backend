using FlavorsOfOliveira.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Services.Interfaces
{
	public interface IRecipeService
	{
		List<Recipe> GetAll();
  Recipe GetById(int id);
  Recipe Save(Recipe recipe);
  void Remove(int id);
	}
}
