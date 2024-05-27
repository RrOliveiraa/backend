using FlavorsOfOliveira.Data.Context;
using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Implementations;
using FlavorsOfOliveira.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlavorsOfOliveira.Controllers
{
	[Route("FlavorsOfOliveiraapi/[controller]")]
	[ApiController]
	public class RecipeController : ControllerBase
	{
		private readonly IRecipeRepository _recipeRepository;
		private readonly IUserRepository _userRepository;
		private readonly FlavorsOfOliveiraDBContext _flavorsOfOliveiraDBContext; 

		public RecipeController(IRecipeRepository recipeRepository, FlavorsOfOliveiraDBContext flavorsOfOliveiraDBContext, IUserRepository? userRepository)
		{
			_recipeRepository = recipeRepository;
			_flavorsOfOliveiraDBContext = flavorsOfOliveiraDBContext;
			_userRepository = userRepository;
		}


		[HttpGet]
		public List<Recipe> GetAll()
		{
			return _recipeRepository.GetAll();
		}

		[HttpGet("RecipeBy{Id}")]
		public Recipe GetById(int Id)
		{
			return _recipeRepository.GetById(Id);
		}


		[HttpPost]
		public Recipe Save(Recipe recipe)
		{
			return Save(recipe);
		}


		[HttpPost("CreateRecipe")]
		public IActionResult CreateRecipe([FromBody] Recipe recipe)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			recipe.IsApprovedByAdmin = false;

		
			if (_recipeRepository.ExistsByTitle(recipe.Title))
			{
				return BadRequest("Recipe already exists");
			}

		
			var user = _userRepository.GetByUsername(recipe.UserName);

			if (user == null)
			{
				return NotFound($"User with username {recipe.UserName} not found.");
			}


	
			var newRecipe = new Recipe
			{
				Title = recipe.Title,
				Description = recipe.Description,
				Difficulty = recipe.Difficulty,
				Duration = recipe.Duration,
				UserId = user.Id, 
    UserName = user.UserName,
    Ingredients = new List<Ingredient>()
			};

			
			foreach (var ingredient in recipe.Ingredients)
			{
				
				var ingredients = new Ingredient
				{
					Name = ingredient.Name,
					Quantity = ingredient.Quantity,
     Unit = ingredient.Unit,
				
				};

				newRecipe.Ingredients.Add(ingredient);
			}

			recipe.UserId = user.Id;
			
			_recipeRepository.Add(newRecipe);

			_flavorsOfOliveiraDBContext.SaveChanges();

			// Retorne uma resposta de sucesso com a nova receita criada
			return CreatedAtAction(nameof(GetById), new { id = newRecipe.Id }, newRecipe);
		}


		[HttpPost("AddFavoriteRecipe")]
		public IActionResult AddFavoriteRecipe(string username, string title)
		{
			var user = _userRepository.GetByUsername(username);
			if (user == null)
			{
				return NotFound($"User with username {username} not found.");
			}

			var recipe = _recipeRepository.GetByTitle(title);
			if (recipe == null)
			{
				return NotFound($"Recipe with title {title} not found.");
			}

			user.FavoriteRecipes.Add(recipe);
			_userRepository.Update(user);

			return Ok($"Recipe with title {title} added to favorites for user with username {username}.");

		}


		[HttpPost("RemoveFavoriteRecipe")]
		public IActionResult RemoveFavoriteRecipe(string username, string title)
		{
			var user = _userRepository.GetByUsername(username);
			if (user == null)
			{
				return NotFound($"User with username {username} not found. ");
			}

			var recipe = _recipeRepository.GetByTitle(title);
			if (recipe == null)
			{
				return NotFound($"Recipe with {title} not found.");
			}

			user.FavoriteRecipes.Remove(recipe);
			_userRepository.Update(user);

			return Ok($"Recipe with title {title} removed from favorites for user with username {username}.");

		}

		
	}
}
