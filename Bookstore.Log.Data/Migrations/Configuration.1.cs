namespace Bookstore.Log.Data.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<Bookstore.Log.Data.BookstoreLogContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(Bookstore.Log.Data.BookstoreLogContext context)
		{
		}
	}
}