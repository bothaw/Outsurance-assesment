using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assesment
{
	public class CsvResponse<T>
	{
		public bool Ok
		{
			get
			{
				return String.IsNullOrEmpty(Error) && Exception == null;
			}
		}

		public string Error { get; set; }
		public Exception Exception { get; set; }
		public List<T> ReturnList { get; set; }

		public CsvResponse()
		{
			ReturnList = new List<T>();
		}

	}
}
