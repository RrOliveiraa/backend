﻿using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Implementations;
using FlavorsOfOliveira.Repository.Interfaces;
using FlavorsOfOliveira.Services.Interfaces;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FlavorsOfOliveira.Controllers
{
	[Route("FlavorsOfOliveiraapi/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{

		private readonly IUserRepository _userRepository;
		private readonly IUserService _userService;
  private readonly IRecipeRepository _recipeRepository;
		public UserController(IUserRepository userRepository, IUserService userService, IRecipeRepository recipeRepository)
		{
			_userRepository = userRepository;
   _userService = userService;
			_recipeRepository = recipeRepository;
		}

		[HttpGet]
		public List<User> GetAll()
		{
			return _userRepository.GetAll();
		}

		[HttpGet("{Id}")]
		public User GetById(int Id)
		{
			return _userRepository.GetById(Id);
		}

		[HttpPost]
		public User Save(User user)
		{
			return Save(user);
		}

		[HttpDelete("{Id}")]
		public void Remove(int Id)
		{
			Remove(Id);
		}


		[HttpPost("Register")]
		public IActionResult Register([FromBody] User user)
		{
			// Valida os dados recebidos
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Verifica se o username já está em uso
			if (_userRepository.ExistsByUsername(user.UserName))
			{
				return BadRequest("Username already exists");
			}

			// Crie um novo objeto User com os dados fornecidos
			var newUser = new User
			{
				UserName = user.UserName,
				Password = user.Password,
				Email = user.Email,
				Name = user.Name,

			};

			// guarda o novo user no banco de dados
			_userRepository.Add(newUser);

			// Retorne uma resposta de sucesso com o novo usuário criado
			return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
		}




		[HttpPost("Login")]
		public IActionResult Login([FromBody] User user)
		{
			// Verificar as credenciais do usuário
			if (_userRepository.AuthenticatedUser(user.UserName, user.Password))
			{
				// Criar um cookie de autenticação
				var cookieOptions = new CookieOptions
				{
					// Define a expiração do cookie (opcional)
					Expires = DateTime.UtcNow.AddHours(1),
					// Indica que o cookie só deve ser enviado em solicitações HTTPS (opcional)
					Secure = true,
					// Indica se o cookie é acessível somente pelo servidor (opcional)
					HttpOnly = true
				};

				// Adicionar o nome do usuário ao cookie de autenticação
				Response.Cookies.Append("UserName", user.UserName, cookieOptions);

				// Retornar uma resposta de sucesso
				return Ok("Login successful!");
			}

			// Se as credenciais forem inválidas, retornar um erro
			return Unauthorized("Invalid username or password");
		}


		[HttpGet("UserInfo")]
		public IActionResult UserInfo()
		{
			// Verifica se o usuário está autenticado
			if (HttpContext.User.Identity.IsAuthenticated)
			{
				// Obtém o nome de usuário do usuário autenticado
				string username = HttpContext.User.Identity.Name;

				var user = _userRepository.GetByUsername(username);

				// Verifica se o usuário foi encontrado
				if (user == null)
				{
					return NotFound("User not found");
				}

				// Retorna as informações do usuário na resposta
				return Ok(new
				{
					Name = user.Name,
					Email = user.Email,
     Username = user.UserName,
     Password = user.Password,
     FavoriteRecipes = user.FavoriteRecipes,
					// Adicione outros detalhes do usuário conforme necessário
				});
			}
			else
			{
				return Unauthorized("User is not authenticated");
			}
		}



		[HttpPost("ForgotPassword")]
		public IActionResult ForgotPassword([FromBody] User user)
		{
			// Verifica se o modelo de usuário recebido é válido
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Verifica se o usuário existe
			var existingUser = _userRepository.GetByUsername(user.UserName);
			if (existingUser == null || existingUser.Email != user.Email)
			{
				// Retorna 404 Not Found se o usuário não for encontrado ou o e-mail não corresponder
				return NotFound("User not found");
			}

			// Atualiza a senha do usuário
			bool success = _userService.UpdatePassword(existingUser, user.Password);
			if (success)
			{
				// Retorne uma resposta de sucesso
				return Ok("Password updated successfully");
			}
			else
			{
				// Retorna um erro interno se houver um problema ao atualizar a senha
				return StatusCode(500, "Failed to update password");
			}
		}


		[HttpPost("CreateRecipe")]
  
  public IActionResult CreateRecipe([FromBody] Recipe recipe) 
  {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			recipe.IsApprovedByAdmin = false;

			// Verifica se o username já está em uso
			if (_recipeRepository.ExistsByTitle(recipe.Title))
			{
				return BadRequest("Recipe already exists");
			}

			// Crie um novo objeto User com os dados fornecidos
			var newRecipe = new Recipe
			{
				Title = recipe.Title,
				Description = recipe.Description,
				Difficulty = recipe.Difficulty,
				Duration = recipe.Duration,
    Ingredients = recipe.Ingredients,

			};

			// guarda o novo user no banco de dados
			_recipeRepository.Add(newRecipe);

			// Retorne uma resposta de sucesso com o novo usuário criado
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

