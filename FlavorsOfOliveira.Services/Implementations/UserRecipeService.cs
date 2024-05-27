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
	public class UserRecipeService : IUserRecipeService
	{
   private readonly IUserRecipeRepository _userrecipeRepository;
   private readonly IRecipeRepository _recipeRepository;
   private readonly IUserRepository _userRepository;

		public UserRecipeService(IUserRecipeRepository userrecipeRepository, IRecipeRepository recipeRepository, IUserRepository userRepository)
		{
			_userrecipeRepository = userrecipeRepository;
			_recipeRepository = recipeRepository;
			_userRepository = userRepository;
		}
		public List<UserRecipe> GetAll()
		{
			 List<UserRecipe> userrecipes = _userrecipeRepository.GetAll();
    
    foreach (UserRecipe userrecipe in userrecipes)
    {
      userrecipe.User = _userRepository.GetById(userrecipe.Id);
      userrecipe.Recipe = _recipeRepository.GetById(userrecipe.Id);
    
		 	}
   return userrecipes;
		}

		public UserRecipe GetById(int id)
		{
			 return _userrecipeRepository.GetById(id);
		}

		public void Remove(int id)
		{
			 UserRecipe userrecipe = _userrecipeRepository.GetById(id);
    _userrecipeRepository.Remove(userrecipe);
		}

		public UserRecipe Save(UserRecipe userrecipe)
		{
			 if (userrecipe.Id == 0)
    {
      return _userrecipeRepository.Add(userrecipe);
			 }
    else  
    {
      return _userrecipeRepository.Update(userrecipe);
    
    }
		}
	}
}
