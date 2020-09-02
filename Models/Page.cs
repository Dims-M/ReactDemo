using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
	/// <summary>
	/// Отвечает за страницу
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Page<T>
	{
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public List<T> Records { get; set; }

		public Page()
		{
			Records = new List<T>();
		}

		public Page(IEnumerable<T> records)
		{
			Records = new List<T>(records);
		}
	}
}
