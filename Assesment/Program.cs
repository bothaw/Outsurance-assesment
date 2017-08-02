using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assesment
{
	class Program
	{
		static string defaultFile = "Resource/data.csv";

		struct Wording
		{
			public const string EnterPath = "Please enter path of user file to load (leave empty to load default file):";
			public const string PressToExit = "Press any key to exit.";
			public const string FindSourceFilesAt = "You can find the test result files in the output directory";
		}

		static void Main(string[] args)
		{
			Console.WriteLine(Wording.EnterPath);
			var inputFile = Console.ReadLine();

			if (string.IsNullOrEmpty(inputFile))
			{
				inputFile = defaultFile;
			}

			var response = CsvHelper.Load<User>(inputFile, firstLineIsHeader: true);

			if (response.Ok)
			{
				PerformTest1(response.ReturnList);
				PerformTest2(response.ReturnList);

				Console.WriteLine();
				Console.WriteLine(Wording.FindSourceFilesAt);
			}
			else
			{
				Console.WriteLine(response.Error);
			}

			Console.WriteLine(Wording.PressToExit);
			Console.ReadKey();
		}

		static void PerformTest1(List<User> users)
		{
			Console.WriteLine("Running Test 1");
			var orderedNamesCount = User.GetOrderedNamesCount(users);

			StringBuilder sb = new StringBuilder();
			foreach (var item in orderedNamesCount)
			{
				sb.Append(string.Format("{0},{1}", item.Key, item.Value));
				sb.Append(Environment.NewLine);
			}

			WriteFile(sb, @"Test1.txt");
		}

		static void PerformTest2(List<User> users)
		{
			Console.WriteLine("Running Test 2");
			var list = User.ListOrderByStreetNames(users);

			StringBuilder sb = new StringBuilder();
			foreach (var item in list)
			{
				sb.Append(string.Format("{0}", item.Address));
				sb.Append(Environment.NewLine);
			}

			WriteFile(sb, @"Test2.txt");
		}

		static void WriteFile(StringBuilder sb, string path)
		{
			try
			{
				string directoryPath = Path.GetDirectoryName(path);

				if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

				File.WriteAllText(path, sb.ToString());
			}
			catch (Exception ex)
			{
				Console.WriteLine("Could not write the file ({0}) Exception: {1}", path, ex.Message);
			}
		}


	}
}
