using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Scraper.Contracts
{
	interface IScrapDataService
	{
		Task GenerateCSV(string start, string end, string currency, StreamWriter sw);
	}
}
