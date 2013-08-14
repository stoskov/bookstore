using System;
using System.Linq;

namespace Bookstore.Log.Model
{
	public class SearchLog
	{
		public int SearchLogId { get; set; }

		public DateTime SearchDate { get; set; }

		public string Query { get; set; }

		public SearchLog()
		{
		}
	}
}