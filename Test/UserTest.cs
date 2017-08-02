using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assesment;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
	[TestClass]
	public class UserTest
	{
		[TestMethod]
		public void ExpectUserToBePopulatedCorrectly()
		{
			User user = new User();
			user.FirstName = "Bobby";
			user.LastName = "TheCat";
			user.Address = "The moon";
			user.PhoneNumber = "124578";

			Assert.AreEqual(user.FirstName, "Bobby");
			Assert.AreEqual(user.LastName, "TheCat");
			Assert.AreEqual(user.Address, "The moon");
			Assert.AreEqual(user.PhoneNumber, "124578");
		}

		[TestMethod]
		public void ListOrderByStreetNamesShouldBeOrderCorrectly()
		{
			List<User> users = new List<User>();
			users.Add(new User { Address = "777 bbb" });
			users.Add(new User { Address = "aaa" });
			users.Add(new User { Address = "xxx" });
			users.Add(new User { Address = "111 ccc" });

			var response = User.ListOrderByStreetNames(users);

			Assert.AreEqual(response[0].Address, "aaa");
			Assert.AreEqual(response[1].Address, "777 bbb");
			Assert.AreEqual(response[2].Address, "111 ccc");
			Assert.AreEqual(response[3].Address, "xxx");
		}

		[TestMethod]
		public void AreNamesOrderedByCountDescAndThenNames()
		{
			List<User> users = new List<User>();
			users.Add(new User { FirstName = "apple", LastName = "pie" });
			users.Add(new User { FirstName = "apple", LastName = "juice" });
			users.Add(new User { FirstName = "brown", LastName = "bread" });
			users.Add(new User { FirstName = "white", LastName = "bread" });
			users.Add(new User { FirstName = "xxx", LastName = "xxx" });
			users.Add(new User { FirstName = "xxx", LastName = "bread" });

			var response = User.GetOrderedNamesCount(users);

			Assert.AreEqual(response[0].Key, "bread");
			Assert.AreEqual(response[0].Value, 3);
			Assert.AreEqual(response[1].Key, "xxx");
			Assert.AreEqual(response[1].Value, 3);
			Assert.AreEqual(response[2].Key, "apple");
			Assert.AreEqual(response[2].Value, 2);
			Assert.AreEqual(response[3].Key, "brown");
			Assert.AreEqual(response[3].Value, 1);
			Assert.AreEqual(response[4].Key, "juice");
			Assert.AreEqual(response[4].Value, 1);
			Assert.AreEqual(response[5].Key, "pie");
			Assert.AreEqual(response[5].Value, 1);
			Assert.AreEqual(response[6].Key, "white");
			Assert.AreEqual(response[6].Value, 1);
		}


	}
}
