using System;
using System.Linq;
using Bookstore.DAL;

namespace Bookstore.Client
{
	public class Bookstore
	{
		static void Main()
		{
			//Task3
			XMLLoader.LoadSimpleBooksData(@"..\..\xml\simple-books.xml");

			//Task4
			XMLLoader.LoadBooksData(@"..\..\xml\complex-books.xml");

			//Task5
			XMLFinder.FindBooks(@"..\..\xml\simple-query1.xml");
			Console.WriteLine("-----------------------------------------------");
			XMLFinder.FindBooks(@"..\..\xml\simple-query2.xml");

			//Task6
			//XMLFinder.FindReviews(@"..\..\xml\reviews-queries.xml", @"..\..\xml\reviews-search-results.xml");
		}
	}
}