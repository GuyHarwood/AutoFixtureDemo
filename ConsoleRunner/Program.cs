using System;
using AutoFix.Demo;
using Ploeh.AutoFixture;

namespace ConsoleRunner
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var fixture = new Fixture();
			fixture.Customizations.Add(new FirstNameBuilder());
			fixture.Customizations.Add(new AddressListBuilder());

			var aNumber = fixture.Create<int>();
			Console.WriteLine("Autofixture initialised aNumber with {0}\n\n\n", aNumber);

			var aString = fixture.Create<string>();
			Console.WriteLine("Autofixture initialised aString with {0}\n\n\n", aString);

			var aPerson = fixture.Create<Person>();
			Console.WriteLine("Autofixture initialised aContact with...\n\n {0}", aPerson);

			const int contactCount = 3;
			var people = fixture.CreateMany<Person>(contactCount);

			Console.WriteLine("Autofixture initialised {0} contacts...\n\n", contactCount);

			foreach (var person in people)
			{
				Console.WriteLine("\n\n{0}", person);
			}

			Console.ReadKey();
		}
	}
}