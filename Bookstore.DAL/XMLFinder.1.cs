using System;
using System.Collections.Generic;
using System.Globalization;
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
				Console.WriteLine("{0} books found:", books.Count());
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

		public static List<List<Review>> FindReviews(string filePath)
		{
			List<List<Review>> result = new List<List<Review>>();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(filePath);

			XmlNodeList queryList = xmlDoc.SelectNodes("review-queries/query");

			foreach (XmlNode queryNode in queryList)
			{
				var searchType = queryNode.Attributes["type"].Value;

				if (searchType == "by-period")
				{
					List<Review> resultByPeriod = FindReviewsByPeriod(queryNode);
					result.Add(resultByPeriod);
				}
				else
				{
					List<Review> resultByAuthor = FindReviewsByAuthor(queryNode);
					result.Add(resultByAuthor);
				}
			}

			return result;
		}

		private static List<Review> FindReviewsByPeriod(XmlNode queryNode)
		{
			DateTime startPeriod = DateTime.ParseExact(queryNode.GetChildText("start-date"), "d-MMM-yyyy", CultureInfo.InvariantCulture);
			DateTime endPeriod = DateTime.ParseExact(queryNode.GetChildText("end-date"), "d-MMM-yyyy", CultureInfo.InvariantCulture);
			List<Review> result = BookstoreDAL.FindReviewsByPeriod(startPeriod, endPeriod);

			BookstoreLogsDAL.LogSearch(queryNode.OuterXml);

			return result;
		}

		private static List<Review> FindReviewsByAuthor(XmlNode queryNode)
		{
			string authorName = queryNode.GetChildText("author-name");
			List<Review> result = BookstoreDAL.FindReviewsByAuthor(authorName);

			BookstoreLogsDAL.LogSearch(queryNode.OuterXml);

			return result;
		}
	}
}