using System;
using System.Data.Entity;
using System.Linq;
using Bookstore.Log.Data.Migrations;
using Bookstore.Log.Model;

namespace Bookstore.Log.Data
{
	public class BookstoreLogContext : DbContext
	{
		public DbSet<SearchLog> SearchLogs { get; set; }

		public BookstoreLogContext()
			: base("BookstoreLogs")
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookstoreLogContext, Configuration>());
		}
	}
}