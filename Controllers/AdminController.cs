 using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlavorsOfOliveira.Repository.Interfaces;
using FlavorsOfOliveira.Repository.Implementations;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using FlavorsOfOliveira.Migrations;

namespace FlavorsOfOliveira.Controllers
{
	[Route("FlavorsOfOliveiraapi/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly FlavorsOfOliveiraDBContext _flavorsOfOliveiraDBcontext;
		private readonly IUserRepository _userRepository;
  private readonly IRecipeRepository _recipeRepository;

		public AdminController(FlavorsOfOliveiraDBContext context, IUserRepository userRepository, IRecipeRepository recipeRepository, FlavorsOfOliveiraDBContext? flavrosOfOliveiraDBcontext)
		{
			_flavorsOfOliveiraDBcontext = flavrosOfOliveiraDBcontext;
			_userRepository = userRepository;
			_recipeRepository = recipeRepository;
		}



		[HttpPost]
		public Admin Save(Admin admin)
		{
			return Save(admin);
		}

		[HttpDelete("DeleteUser")]
		public IActionResult Remove(int Id)
		{
			// Verifica se o user com o ID fornecido existe
			var user = _userRepository.GetById(Id);
			if (user == null)
			{
				return NotFound("User not found");
			}

			try
			{
				// Remove o user
				_userRepository.Remove(user);
				return Ok("User removed successfully");
			}
			catch (Exception ex)
			{
				// Handle exception
				return StatusCode(500, "Failed to remove user");
			}
		}

		[HttpDelete("DeleteRecipe/{id}")]
		public IActionResult DeleteRecipe(int id)
		{
			// Procurar a receita pelo id
			var recipe = _recipeRepository.GetById(id);

			// Verificar se a receita existe
			if (recipe == null)
			{
				return NotFound($"Recipe with ID {id} not found.");
			}

			try
			{
				// Remover a receita
				_recipeRepository.Remove(recipe);
				_flavorsOfOliveiraDBcontext.SaveChanges();

				// Retorna uma resposta de sucesso
				return Ok($"Recipe with ID {id} has been deleted successfully.");
			}
			catch (Exception ex)
			{
				// Se ocorrer um erro ao excluir a receita, retornar um status de erro
				return StatusCode(500, $"An error occurred while deleting the recipe: {ex.Message}");
			}
		}

		[HttpPost("Login")]
		
		public IActionResult Login([FromBody] Admin admin)
		{
			var authenticatedAdmin = _flavorsOfOliveiraDBcontext.Admins.SingleOrDefault(a => a.UserName == admin.UserName && a.Password == admin.Password);

			if (authenticatedAdmin != null)
			{
				// Admin autenticado com sucesso
				return Ok("Login successfully! Welcome!");
			}

			// Credenciais inválidas
			return Unauthorized("Login failed, try again!");
		}

		
		[HttpGet("Users")]
		

		public IActionResult GetRegisteredUsers()
		{
			var users = _userRepository.GetAll();

			return Ok(users);
		}



		[HttpPost("BlockUser")]
		public IActionResult BlockUser(string username)
		{
			var user = _userRepository.GetByUsername(username);
			if (user == null)
			{
				return NotFound($"User with ID {username} not found.");
			}

			user.IsBlocked = true;
			_userRepository.Update(user);

			return Ok($"User with ID {username} has been blocked.");
		}

  [HttpPost("UnblockUser")]
  public IActionResult DisblockUser(string username) 
  {
			var user = _userRepository.GetByUsername(username);
   if (user == null)
   {
				return NotFound($"User wtih ID {username} not found.");
   }
   
   user.IsBlocked = false;
   _userRepository.Update(user);
			return Ok($"User with {username} has been unlocked.");

  }



		[HttpPost("AproveRecipe")]
		public IActionResult ApproveRecipe(int Id)
		{
			var recipe = _recipeRepository.GetById(Id);
			if (recipe == null)
			{
				return NotFound();
			}

			recipe.IsApprovedByAdmin = true; // Aprova a receita
			_recipeRepository.Update(recipe);

			return Ok($"Recipe with ID {Id} has been approved.");
		}



		[HttpPost("RejectRecipe")]
		public IActionResult RejectRecipe(int Id)
		{
			var recipe = _recipeRepository.GetById(Id);
			if (recipe == null)
			{
				return NotFound();
			}

			recipe.IsApprovedByAdmin = false; // Rejeita a receita
			_recipeRepository.Update(recipe);

			return Ok($"Recipe with ID {Id} has been rejected.");
		}


		
		
  [HttpPost("EditRecipe")]
		public IActionResult EditRecipe(int Id, [FromBody] Recipe updatedRecipe)
		{
			// Verifique se a receita existe
			var existingRecipe = _recipeRepository.GetById(Id);
			if (existingRecipe == null)
			{
				return NotFound($"Recipe with ID {Id} not found");
			}
			else
			{

				// Atualize os detalhes da receita existente com os novos detalhes
				existingRecipe.Title = updatedRecipe.Title;
				existingRecipe.Description = updatedRecipe.Description;
				existingRecipe.Ingredients = updatedRecipe.Ingredients;
				existingRecipe.Duration = updatedRecipe.Duration;
				existingRecipe.Difficulty = updatedRecipe.Difficulty;
				existingRecipe.IsApprovedByAdmin = updatedRecipe.IsApprovedByAdmin; // Talvez o admin queira alterar isso

				// gaurda as alteraçoes
				_recipeRepository.Update(existingRecipe);

				return Ok("Recipe updated successfully");

			}
		}

		[HttpGet("PendingRecipes")]
		public IActionResult GetPendingRecipes()
		{
			// Obtém a lista de receitas pendentes
			var pendingRecipes = _recipeRepository.GetPendingRecipes();

			// Verifica se existem receitas pendentes
			if (pendingRecipes.Count == 0)
			{
				return NotFound("No pending recipes found");
			}

			// Retorna a lista de receitas pendentes
			return Ok(pendingRecipes);
		}




	}
}
  


