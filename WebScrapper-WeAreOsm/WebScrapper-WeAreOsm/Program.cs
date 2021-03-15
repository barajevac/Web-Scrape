using System;
using System.Linq;
using HtmlAgilityPack;
using System.Net.Http;

namespace Web_Scraper
{
	class Program
	{
		static void Main(string[] args)
		{
			Startup.Run(args[0], args[1], args[2]);
			Console.ReadLine();
		}
		
	}
}
