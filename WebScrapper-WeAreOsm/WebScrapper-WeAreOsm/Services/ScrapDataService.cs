using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Web_Scraper.Contracts;

namespace Web_Scraper.Services
{
	class ScrapDataService : IScrapDataService
	{
		private static string _baseUrl = "https://srh.bankofchina.com/search/whpj/searchen.jsp?";
		private int numberOfPages;

		public string GenerateCsvHeader(string[] data)
		{
			var CsvDataHeader = "";

			for (int i = 0; i < 7; i++)
			{
				if (!data[i].Equals(""))
				{
					CsvDataHeader += data[i].ToString().Trim() + ",";
				}

				if (((i + 1) % 7 == 0))
				{
					CsvDataHeader = CsvDataHeader.Remove(CsvDataHeader.LastIndexOf(','), 1);
					break;
				}
			}
			return CsvDataHeader;
		}

		public async Task GenerateCSV(string start, string end, string currency, StreamWriter sw)
		{
			

			var csvHeader = "";
			var csvDataRow = "";
			int numberOfPages = await CountNumberOfPages(start, end, currency, "1");

			Console.WriteLine("Total Pages : " + numberOfPages + "\n");

			for (int i = 0; i < numberOfPages; i++)
			{
				var pageNumber = (i + 1).ToString();
				Console.WriteLine("Page " + pageNumber + " in progress...");

				var data = await SplitData(start, end, currency, pageNumber);

				if (csvHeader.Equals(""))
				{
					csvHeader = GenerateCsvHeader(data);
					sw.WriteLine(csvHeader);
				}

				for (int j = 7; j < data.Length; j++)
				{
					if (!data[j].Equals(""))
					{
						csvDataRow += data[j].ToString().Trim() + ",";
					}

					if (((j + 1) % 7 == 0))
					{
						csvDataRow = csvDataRow.Remove(csvDataRow.LastIndexOf(','), 1);
						sw.WriteLine(csvDataRow);
						csvDataRow = "";
					}
				}

				Console.WriteLine("Page " + pageNumber + " fetched.\n");
			}
			Console.WriteLine("All data is fetched. Please check the file on specified location.\n");
			System.Environment.Exit(0);
			sw.Close();
		}

		private async Task<string[]> SplitData(string start, string end, string currency, string pageNumber)
		{
			var CurrencyRateData = await FetchData(start, end, currency, pageNumber);
			var data = Regex.Replace(CurrencyRateData, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
			var splitedData = data.Split('\n');
			return splitedData;
		}

		private async Task<string> FetchData(string start, string end, string currency, string page)
		{
			var url = CreateUrl(start, end, currency, page);
			var HttpClient = new HttpClient();
			var html = await HttpClient.GetStringAsync(url);

			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			var CurrencyRatePage = htmlDocument.DocumentNode.Descendants("table").ToList();
			var CurrencyRateData = CurrencyRatePage[2].InnerText;
			return CurrencyRateData;
		}

		private async Task <int> CountNumberOfPages(string start, string end, string currency, string page)
		{

			var url = CreateUrl(start, end, currency, page);
			var HttpClient = new HttpClient();
			var html = await HttpClient.GetStringAsync(url);

			var htmlLines = html.Split('\n');
			var totalRecords = 0;
			foreach (var line in htmlLines)
			{
				if (line.Contains("var m_nRecordCount = "))
				{
					var split = line.Split('=');
					var val = split[1];
					var val2 = val.Split(';');
					var result = val2[0].ToString().Trim();
					totalRecords =  int.Parse(result);
					break;
				}
			}

			if (totalRecords % 20 == 0)
			{
				int numberOfPages = totalRecords / 20;
				return numberOfPages;
			}
			else
			{
				int numberOfPages = totalRecords / 20;
				numberOfPages = numberOfPages + 1;
				return numberOfPages;
			}
			
		}

		private string CreateUrl(string start, string end, string currency, string page)
			=> _baseUrl + $"erectDate={start}&nothing={end}&pjname=EUR&page={page}";
	}
}
