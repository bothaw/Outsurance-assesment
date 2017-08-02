using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assesment
{
	/// <summary>
	/// Helper class to easily load data from CSV file to List<T>
	/// </summary>
	public class CsvHelper
	{
		private static void ValidatePath<T> (string path, CsvResponse<T> response)
		{
			if (string.IsNullOrEmpty(path))
			{
				response.Error = "Path cannot be empty";
			}
		}

		private static void ValidateDelimeter<T>(string delimeter, CsvResponse<T> response)
		{
			if (string.IsNullOrEmpty(delimeter))
			{
				response.Error = "Delimiter cannot be empty";
			}
		}
		
		public static CsvResponse<T> Load<T>(string path, string delimeter = ",", bool firstLineIsHeader = false) where T: ICsvObject
		{
			var response = new CsvResponse<T>();

			if (response.Ok) { ValidatePath<T>(path, response); }
			if (response.Ok) { ValidateDelimeter<T>(delimeter, response); }

			if (response.Ok)
			{
				try
				{
					using (StreamReader reader = new StreamReader(path))
					{
						if (firstLineIsHeader)
						{
							reader.ReadLine();
						}

						string line;

						while ((line = reader.ReadLine()) != null)
						{
							try
							{
								var splitResult = line.Split(new string[] { delimeter }, StringSplitOptions.None);

								T obj = Activator.CreateInstance<T>();
								obj.Parse(splitResult);
								response.ReturnList.Add(obj);
							}
							catch
							{
								//ignore error / invalid data and continue to next line
							}
						}
					}

				}
				catch (FileNotFoundException notFoundEx)
				{
					response.Error = "File not found";
					response.Exception = notFoundEx;
				}
				catch (Exception ex)
				{
					response.Error = "Unexpected error occured.";
					response.Exception = ex;
				}
			}
			
			return response;
		}
				
	}
}
