using Bookstore.Log.Data;
using Bookstore.Log.Model;
using System;
using System.Linq;

namespace Bookstore.DAL
{
	public static class BookstoreLogsDAL
	{
		public static void LogSearch(string query)
		{
			SearchLog log = new SearchLog();
			log.SearchDate = DateTime.Now;
			log.Query = query;

			BookstoreLogContext context = new BookstoreLogContext();
			context.SearchLogs.Add(log);
			context.SaveChanges();
		}
	}
}