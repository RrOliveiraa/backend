using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using FlavorsOfOliveira.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Services.Implementations
{
	public class RecipeService : IRecipeService
	{
  private readonly IRecipeRepository _recipeRepository;

  public RecipeService(IRecipeRepository recipeRepository)
		{
			_recipeRepository = recipeRepository;
		}

		public List<Recipe> GetAll()
		{
			 return _recipeRepository.GetAll();
		}

		public Recipe GetById(int id)
		{
			 return _recipeRepository.GetById(id);
		}

		public void Remove(int id)
		{
			 Recipe recipe = _recipeRepository.GetById(id);
    _recipeRepository.Remove(recipe);
		}

		public Recipe Save(Recipe recipe)
		{
			 if(recipe.Id == 0)
    {
      return _recipeRepository.Add(recipe);
    }
    else 
    {
      return _recipeRepository.Update(recipe);   
			  
    }
		}
	}
}
