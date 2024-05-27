using FlavorsOfOliveira.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Services.Interfaces
{
	public interface IAdminService
	{

		List<Admin> GetAll();
  Admin GetById(int id);
  Admin Save (Admin admin);
  void Remove (int id);
  
 }
}
