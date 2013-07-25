using System;
using System.Linq;
using Bookstore.Data;
using System.Collections.Generic;

namespace Bookstore.DAL
{
	public class BookstoreDAL
	{
		public static Book AddBook(string title, string isbn, decimal? price, string webSite, IEnumerable<string> authors)
		{
			Book book = new Book();
			book.Title = title;
			book.ISBN = isbn;
			book.Price = price;
			book.WebSite = webSite;

			BookstoreEntities context = new BookstoreEntities();

			foreach (string authorName in authors)
			{
				Author author = CreateOrLoadAuthor(context, authorName);
				book.Authors.Add(author);
			}

			context.Books.Add(book);
			context.SaveChanges();

			return book;
		}

		public static Review AddReview(int? authorId, int? bookId, DateTime reviewDate, string reviewContent)
		{
			BookstoreEntities context = new BookstoreEntities();
			Review review = new Review();
			review.AuthorId = authorId;
			review.BookId = bookId;
			review.Date = reviewDate;
			review.Text = reviewContent;

			context.Reviews.Add(review);
			context.SaveChanges();

			return review;
		}

		public static Author AddAuthor(string name)
		{
			BookstoreEntities context = new BookstoreEntities();
			Author author = CreateOrLoadAuthor(context, name);

			return author;
		}

		public static Author CreateOrLoadAuthor(BookstoreEntities context, string name)
		{
			Author existingAuthor =
				(from a in context.Authors
				 where a.Name.ToLower() == name.ToLower()
				 select a).FirstOrDefault();

			if (existingAuthor != null)
			{
				return existingAuthor;
			}

			Author newAuthor = new Author();
			newAuthor.Name = name;

			context.Authors.Add(newAuthor);
			context.SaveChanges();

			return newAuthor;
		}

		public static List<Book> FindBooksByTitleAuthorIsbn(string title, string authorName, string isbn)
		{
			BookstoreEntities context = new BookstoreEntities();

			var booksQuery = from b in context.Books
							 select b;

			if (title != null)
			{
				booksQuery = from b in context.Books
							 where b.Title.ToLower() == title.ToLower()
							 select b;
			}

			if (isbn != null)
			{
				booksQuery = booksQuery.Where(b => b.ISBN == isbn);
			}

			if (authorName != null)
			{
				booksQuery = booksQuery.Where(
					b => b.Authors.Any(a => a.Name.ToLower() == authorName.ToLower()));
			}

			booksQuery = booksQuery.OrderBy(b => b.Title);

			return booksQuery.ToList();
		}
	}
}