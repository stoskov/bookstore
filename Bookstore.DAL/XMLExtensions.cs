using System;
using System.Linq;
using System.Xml;

namespace Bookstore.DAL
{
	public static class XMLExtensions
	{
		public static string GetChildText(this XmlNode node, string tagName)
		{
			XmlNode childNode = node.SelectSingleNode(tagName);
			if (childNode == null)
			{
				return null;
			}
			return childNode.InnerText.Trim();
		}
	}
}