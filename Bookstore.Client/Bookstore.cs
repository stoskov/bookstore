using System;
using System.Linq;
using Bookstore.DAL;

namespace Bookstore.Client
{
	public class Bookstore
	{
		static void Main()
		{
			XMLLoader.LoadSimpleBooksData(@"..\..\data\simple-books.xml");
			XMLLoader.LoadBooksData(@"..\..\data\complex-books.xml");
			XMLFinder.SimpleSearch(@"..\..\data\simple-query1.xml");
			XMLFinder.SimpleSearch(@"..\..\data\simple-query2.xml");
		}
	}
}