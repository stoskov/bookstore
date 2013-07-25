using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Bookstore.Data;

namespace Bookstore.DAL
{
	public static class XMLFinder
	{
		public static void SimpleSearch(string filePath)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(filePath);

			string title = xmlDoc.GetChildText("query/title");
			string authorName = xmlDoc.GetChildText("query/author");
			string isbn = xmlDoc.GetChildText("query/isbn");

			List<Book> books = BookstoreDAL.FindByTitleAuthorIsbn(title, authorName, isbn);
			if (books.Count > 0)
			{
				foreach (Book book in books)
				{
					Console.WriteLine(book.Title);
				}
			}
			else
			{
				Console.WriteLine("Nothing found");
			}
		}
	}
}