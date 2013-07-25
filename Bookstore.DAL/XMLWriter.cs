using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using Bookstore.Data;

namespace Bookstore.DAL
{
	public static class XMLWriter
	{
		public static void WriteReviewsToFile(List<List<Review>> searchResult, string filePath)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			using (XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8))
			{
				writer.Formatting = Formatting.Indented;
				writer.IndentChar = '\t';
				writer.Indentation = 1;

				writer.WriteStartDocument();

				//<search-results>
				writer.WriteStartElement("search-results");

				foreach (List<Review> resultSet in searchResult)
				{
					writer.WriteStartElement("results-set");

					foreach (Review review in resultSet)
					{
						//<review>
						writer.WriteStartElement("review");

						//<date></date>
						writer.WriteElementString("date", review.Date.ToString("dd-MMM-yyyy"));
						//<content></content>
						writer.WriteElementString("content", review.Text);

						//<book>
						writer.WriteStartElement("book");

						//<title></title>
						writer.WriteElementString("title", review.Book.Title);

						if (review.Book.Authors.Count() > 0)
						{
							string authors = string.Join(", ", review.Book.Authors.OrderBy(a => a.Name).Select(a => a.Name).ToList());
							//<authors></authors>
							writer.WriteElementString("authors", authors);
						}
						
						string isbn = review.Book.ISBN;
						if (!string.IsNullOrEmpty(isbn))
						{
							//<isbn></isbn>
							writer.WriteElementString("isbn", review.Book.ISBN);
						}

						//<url></url>
						writer.WriteElementString("url", review.Book.WebSite);

						//</book>
						writer.WriteEndElement();

						//</review>
						writer.WriteEndElement();
					}

					//</search-results>
					writer.WriteEndElement();
				}

				writer.WriteEndDocument();
			}
		}
	}
}