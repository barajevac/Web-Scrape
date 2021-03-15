using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Web_Scraper.Contracts;
using Web_Scraper.Services;

namespace Web_Scraper
{
	public class Startup
	{
		private static IScrapDataService _scrapDataService;
		private static FileService _fileService;
		private static StreamWriter _streamWriter;

		private static string ExtractionPath = @"C:\Users\Barajevac\Desktop";

		public static async void Run(string start, string end, string currency)
		{
			_fileService = new FileService();
			var filePath = _fileService.CreateFileIfDoesNotExist(ExtractionPath, currency, start, end);

			_streamWriter = new StreamWriter(filePath);
			_scrapDataService = new ScrapDataService();
			await _scrapDataService.GenerateCSV(start, end, currency, _streamWriter);
			

		}

	}
}
