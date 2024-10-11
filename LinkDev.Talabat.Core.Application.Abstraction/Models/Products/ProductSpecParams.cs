﻿namespace LinkDev.Talabat.Core.Application.Abstraction.Products
{
	public class ProductSpecParams
	{
		private string? searsh;
		public string? Sort {  get; set; }
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }
		public string? Search
		{
			get { return searsh; }
			set { searsh = value?.ToUpper(); }
		}
		public int PageIndex { get; set; } = 1;
		private const int MaxPageSize = 10;
		private int pageSize = 5;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value>MaxPageSize? MaxPageSize:value; }
		}
	}
}
