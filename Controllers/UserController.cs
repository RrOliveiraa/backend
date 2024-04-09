using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlavorsOfOliveira.Controllers
{
	[Route("FlavorsOfOliveiraapi/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{

		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpGet]
		public List<User> GetAll()
		{
			return GetAll();
		}

		[HttpGet("{Id}")]
		public User GetById(int Id)
		{
			return GetById(Id);
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



	}
}
