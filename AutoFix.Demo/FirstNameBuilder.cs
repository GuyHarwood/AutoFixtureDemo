using System;
using System.Reflection;
using Ploeh.AutoFixture.Kernel;

namespace AutoFix.Demo
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
}