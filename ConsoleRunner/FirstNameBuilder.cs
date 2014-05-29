using System;
using System.Collections.Generic;
using System.Reflection;
using AutoFix.Demo;
using Ploeh.AutoFixture.Kernel;

namespace ConsoleRunner
{
	public class FirstNameBuilder : ISpecimenBuilder
	{
		private static readonly string[] Names = {"Chris", "Luke", "Joe", "Becky", "Jason", "Darren"};
		private readonly Random random = new Random();

		public object Create(object request, ISpecimenContext context)
		{
			var pi = request as PropertyInfo;
			if (pi == null)
			{
				return new NoSpecimen(request);
			}
			if (pi.PropertyType != typeof (string)
			    || pi.Name != "Name")
			{
				return new NoSpecimen(request);
			}

			var index = random.Next(0, Names.Length - 1);
			return Names[index];
		}
	}

	public class AddressListBuilder : ISpecimenBuilder
	{
		private static readonly string[] Cities = {"London", "Nottingham", "Sheffield", "Morecambe", "Manchester", "Skegness"};
		private readonly Random random = new Random();

		public object Create(object request, ISpecimenContext context)
		{
			var pr = request as ParameterInfo;
			if (pr == null)
			{
				return new NoSpecimen(request);
			}

			if (pr.ParameterType == typeof (IEnumerable<Address>))
			{
				var count = random.Next(0, Cities.Length - 1);
				var cities = new List<Address>();

				for (int i = 0; i < count; i++)
				{
					var index = random.Next(0, Cities.Length - 1);
					cities.Add(new Address()
					{
						City = Cities[index]
					});
				}
				return cities.ToArray();
			}

			return new NoSpecimen(request);
		}
	}
}