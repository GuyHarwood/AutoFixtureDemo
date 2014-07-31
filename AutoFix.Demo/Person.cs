using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoFix.Demo
{
	public class Person
	{
		private readonly IEnumerable<Address> addresses;

		public Person(IEnumerable<Address> addresses)
		{
			this.addresses = addresses;
		}

		public Guid PersonId { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }

		public Address PrimaryAddress
		{
			get { return addresses.FirstOrDefault(add => add.Primary); }
		}

		public override string ToString()
		{
			var cities = new StringBuilder();
			foreach (var address in addresses)
			{
				cities.Append(address.City);
				cities.AppendLine();
			}
			return string.Format("PersonId:{0}\nName:{1}\nAge:{2}\nCities:\n{3}", PersonId, Name, Age,cities);
		}
	}

	public class Address
	{
		public string City { get; set; }
		public bool Primary { get; set; }
	}
}