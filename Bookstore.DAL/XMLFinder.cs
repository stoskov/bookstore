using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Bookstore.Data;

namespace Bookstore.DAL
{
	public static class XMLFinder
	{
		public static void FindBooks(string filePath)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(filePath);

			string title = xmlDoc.GetChildText("query/title");
			string authorName = xmlDoc.GetChildText("query/author");
			string isbn = xmlDoc.GetChildText("query/isbn");

			List<Book> books = BookstoreDAL.FindBooksByTitleAuthorIsbn(title, authorName, isbn);
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

		//public static void FindReviews(string queryPath, string resultPath)
		//{
		//	XmlDocument xmlDoc = new XmlDocument();
		//	xmlDoc.Load(queryPath);

		//	XmlNodeList queryList = xmlDoc.SelectNodes("query");

		//	foreach (XmlNode queryNode in queryList)
		//	{
		//		var authorNameAttribure = reviewNode.Attributes["author"];

		//		string authorName = queryNode.GetChildText("author");
		//		VerifyNodeExistance("author", authorName);

		//		string title = queryNode.GetChildText("title");
		//		VerifyNodeExistance("title", title);

		//		string isbn = queryNode.GetChildText("isbn");
		//		string webSite = queryNode.GetChildText("web-site");

		//		decimal? price = null;
		//		string priceString = queryNode.GetChildText("price");
		//		if (priceString != null)
		//		{
		//			price = decimal.Parse(priceString);
		//		}

		//		List<string> authors = new List<string>();
		//		authors.Add(authorName);

		//		Book book = BookstoreDAL.AddBook(title, isbn, price, webSite, authors);
		//	}

	}
}