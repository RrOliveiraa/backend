﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Domain.Entities
{
  public class Ingredient
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public decimal Quantity { get; set; }
			public string Unit { get; set; }

		}
	}

