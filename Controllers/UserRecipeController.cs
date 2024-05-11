using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Implementations;
using FlavorsOfOliveira.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlavorsOfOliveira.Controllers
{
	[Route("FlavorsOfOliveiraapi/[controller]")]
	[ApiController]
	public class UserRecipeController : ControllerBase
	{

		private readonly IUserRecipeRepository _userRecipeRepository;
		private readonly IRecipeRepository _recipeRepository;

		public UserRecipeController(IUserRecipeRepository userRecipeRepository, IRecipeRepository recipeRepository)
		{
			_userRecipeRepository = userRecipeRepository;
			_recipeRepository = recipeRepository;
		}


		[HttpGet]
		public List<UserRecipe> GetAll()
		{
			return GetAll();
		}

		[HttpGet("{Id}")]
		public UserRecipe GetById(int Id)
		{
			return GetById(Id);
		}

		[HttpPost]
		public UserRecipe Save(UserRecipe userrecipe)
		{
			return Save(userrecipe);
		}

		[HttpDelete("{Id}")]
		public void Remove(int Id)
		{
			Remove(Id);
		}

		[HttpPost("CommentAndRateRecipe")]
		public IActionResult CommentAndRateRecipe(int Id, decimal rating, string comments)
		{
			// Verifique se o usuário está autenticado e obtenha o ID do usuário

			// Verifique se a receita existe
			var recipe = _recipeRepository.GetById(Id);
			if (recipe == null)
			{
				return NotFound($"Recipe with ID {Id} not found.");
			}

			// Salve o comentário e a classificação
			var userRecipe = new UserRecipe
			{
				Recipe = recipe,
				Rating = rating,
				Comments = comments
			};

			_userRecipeRepository.Add(userRecipe);

			return Ok("Comment and rating added successfully.");
		}
	}
}
