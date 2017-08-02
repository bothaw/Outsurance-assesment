using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assesment
{
	public class User: ICsvObject
	{
		static readonly Regex rx = new Regex(@"^\d+\p{Zs}", RegexOptions.Compiled);

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }

		public void Parse(string[] csvLine)
		{
			FirstName   = csvLine[0];
			LastName    = csvLine[1];
			Address     = csvLine[2];
			PhoneNumber = csvLine[3];
		}

		public static List<User> ListOrderByStreetNames(List<User> users)
		{
			var list = users.Select(p => new { User = p, OrderClause = rx.Replace(p.Address, "") })
				.OrderBy(p => p.OrderClause)
				.Select(p => p.User)
				.ToList();

			return list;
		}

		public static List<KeyValuePair<string, int>> GetOrderedNamesCount(List<User> users)
		{
			var list = new List<KeyValuePair<string, int>>();

			list = users.Select(p => p.FirstName)
					.Concat(users.Select(p => p.LastName))
					.GroupBy(p => p)
					.Select(p => new KeyValuePair<string, int>(p.Key, p.Count()))
					.OrderByDescending(o => o.Value)
					.ThenBy(o => o.Key)
					.ToList();

			return list;
		}
	}
}
