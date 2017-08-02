using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assesment;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
	[TestClass]
	public class CsvHelperTest
	{
		[TestMethod]
		public void ReturnCorrectErrorWhenFileNotFound()
		{
			var response = CsvHelper.Load<User>("somemissingFile.txt");
			Assert.AreEqual(response.Error, "File not found");
		}

		[TestMethod]
		public void ReturnCorrectErrorWhenPathIsEmpty()
		{
			var response = CsvHelper.Load<User>(null);
			Assert.AreEqual(response.Error, "Path cannot be empty");
		}

		[TestMethod]
		public void ReturnCorrectErrorWhenDelimeterIsEmpty()
		{
			var response = CsvHelper.Load<User>("somepath.txt",null);
			Assert.AreEqual(response.Error, "Delimiter cannot be empty");
		}

		[TestMethod]
		public void TotalItemCountShouldBe9WithoutHeader()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv");
			Assert.AreEqual(response.ReturnList.Count, 9);
		}

		[TestMethod]
		public void TotalItemCountShouldBe8WithHeader()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv", firstLineIsHeader: true);
			Assert.AreEqual(response.ReturnList.Count, 8);
		}

		[TestMethod]
		public void ReturnNoErrorsWhenValidParamsAreGiven()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv");
			Assert.IsNull(response.Error);
			Assert.IsNull(response.Exception);
			Assert.IsTrue(response.Ok);
		}

		[TestMethod]
		public void ReturnFalseWhenErrorIsSet()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv");
			response.Error = "some error";
			Assert.IsFalse(response.Ok);
		}

		[TestMethod]
		public void ReturnFalseWhenExpectionIsSet()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv");
			response.Exception = new Exception();
			Assert.IsFalse(response.Ok);
		}
		
		[TestMethod]
		public void ExpectHeaderWhenFirstLineHeaderIsFalse()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv");
			var user = response.ReturnList[0];

			Assert.AreEqual(user.FirstName, "FirstName");
			Assert.AreEqual(user.LastName, "LastName");
			Assert.AreEqual(user.Address, "Address");
			Assert.AreEqual(user.PhoneNumber, "PhoneNumber");
		}

		[TestMethod]
		public void ExpectNoHeaderWhenFirstLineHeaderIsTrue()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv", firstLineIsHeader: true);
			var user = response.ReturnList[0];

			Assert.AreNotEqual(user.FirstName, "FirstName");
			Assert.AreNotEqual(user.LastName, "LastName");
			Assert.AreNotEqual(user.Address, "Address");
			Assert.AreNotEqual(user.PhoneNumber, "PhoneNumber");
		}

		[TestMethod]
		public void ReadFirstLine()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv", firstLineIsHeader: true);
			var user = response.ReturnList.First();
			
			Assert.AreEqual(user.FirstName, "Jimmy");
			Assert.AreEqual(user.LastName, "Smith");
			Assert.AreEqual(user.Address, "102 Long Lane");
			Assert.AreEqual(user.PhoneNumber, "29384857");
		}

		[TestMethod]
		public void ReadLastLine()
		{
			var response = CsvHelper.Load<User>("Resource/data.csv", firstLineIsHeader: true);
			var user = response.ReturnList.Last();

			Assert.AreEqual(user.FirstName, "Graham");
			Assert.AreEqual(user.LastName, "Brown");
			Assert.AreEqual(user.Address, "94 Roland St");
			Assert.AreEqual(user.PhoneNumber, "8766556");
		}

	}
}
