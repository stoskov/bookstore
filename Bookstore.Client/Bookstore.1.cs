using System;
using System.Collections.Generic;
using System.Linq;
using Bookstore.DAL;
using Bookstore.Data;

namespace Bookstore.Client
{
	public class Bookstore
	{
		static void Main()
		{
			//Task1
			//In the SQL server

			//Task2
			//In the SQL server
			
			//Task3
			XMLLoader.LoadSimpleBooksData(@"..\..\xml\simple-books.xml");

			//Task4
			XMLLoader.LoadBooksData(@"..\..\xml\complex-books.xml");

			//Task5
			XMLFinder.FindBooks(@"..\..\xml\simple-query1.xml");
			Console.WriteLine("-----------------------------------------------");
			XMLFinder.FindBooks(@"..\..\xml\simple-query2.xml");

			//Task6
			List<List<Review>> searchResult = XMLFinder.FindReviews(@"..\..\xml\reviews-queries.xml");
			XMLWriter.WriteReviewsToFile(searchResult, @"..\..\xml\reviews-search-results.xml");

			//Task7
			//in Bookstore.Log.Data, Bookstore.Log.Model and BookstoreLogsDAL.LogSearch
		}
	}
}