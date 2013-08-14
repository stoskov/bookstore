using Bookstore.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Xml;

namespace Bookstore.DAL
{
	public static class XMLLoader
	{
		public static void LoadSimpleBooksData(string filePath)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(filePath);

			TransactionScope transaction = new TransactionScope(
				TransactionScopeOption.Required,
				new TransactionOptions()
				{
					IsolationLevel = IsolationLevel.RepeatableRead
				});

			using (transaction)
			{
				string xPathQuery = "/catalog/book";
				XmlNodeList booksList = xmlDoc.SelectNodes(xPathQuery);

				foreach (XmlNode bookNode in booksList)
				{
					string authorName = bookNode.GetChildText("author");
					VerifyNodeExistance("author", authorName);

					string title = bookNode.GetChildText("title");
					VerifyNodeExistance("title", title);

					string isbn = bookNode.GetChildText("isbn");
					string webSite = bookNode.GetChildText("web-site");

					decimal? price = null;
					string priceString = bookNode.GetChildText("price");
					if (priceString != null)
					{
						price = decimal.Parse(priceString);
					}

					List<string> authors = new List<string>();
					authors.Add(authorName);

					Book book = BookstoreDAL.AddBook(title, isbn, price, webSite, authors);
				}

				transaction.Complete();
			}
		}

		public static void LoadBooksData(string filePath)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(filePath);

			TransactionScope transaction = new TransactionScope(
				TransactionScopeOption.Required,
				new TransactionOptions()
				{
					IsolationLevel = IsolationLevel.ReadUncommitted
				});

			using (transaction)
			{
				string xPathQuery = "/catalog/book";
				XmlNodeList booksList = xmlDoc.SelectNodes(xPathQuery);

				foreach (XmlNode bookNode in booksList)
				{
					string title = bookNode.GetChildText("title");
					VerifyNodeExistance("title", title);

					string isbn = bookNode.GetChildText("isbn");
					string webSite = bookNode.GetChildText("web-site");

					decimal? price = null;
					string priceString = bookNode.GetChildText("price");
					if (priceString != null)
					{
						price = decimal.Parse(priceString);
					}

					List<string> authors = new List<string>();
					foreach (XmlNode authorNode in bookNode.SelectNodes("authors/author"))
					{
						string authorName = (authorNode.InnerText.Trim());
						if (string.IsNullOrEmpty(authorName))
						{
							continue;
						}

						authors.Add(authorName);
					}

					Book book = BookstoreDAL.AddBook(title, isbn, price, webSite, authors);

					foreach (XmlNode reviewNode in bookNode.SelectNodes("reviews/review"))
					{
						int? authorId = null;
						var authorNameAttribure = reviewNode.Attributes["author"];
						if (authorNameAttribure != null)
						{
							Author author = BookstoreDAL.AddAuthor(authorNameAttribure.Value.Trim());
							authorId = author.AuthorId;
						}

						DateTime reviewDate = DateTime.Now;
						var reviewDateAttribure = reviewNode.Attributes["date"];
						if (reviewDateAttribure != null)
						{
							reviewDate = DateTime.ParseExact(reviewDateAttribure.Value.Trim(), "d-MMM-yyyy", CultureInfo.InvariantCulture);
						}

						string reviewContent = (reviewNode.InnerText.Trim());

						Review review = BookstoreDAL.AddReview(authorId, book.BookId, reviewDate, reviewContent);
					}
				}

				transaction.Complete();
			}
		}


		private static void VerifyNodeExistance(string nodeName, string nodeValue)
		{
			if (nodeValue == null)
			{
				string message = string.Format("Node \"{0}\" is mandatory.", nodeName);
				throw new ArgumentNullException(message);
			}
		}


		private static void VerifyNodeListExistance(string nodeName, IEnumerable<string> nodeValues)
		{
			if (nodeValues.Count() == 0)
			{
				string message = string.Format("Node \"{0}\" is mandatory.", nodeName);
				throw new ArgumentNullException(message);
			}
		}
	}
}