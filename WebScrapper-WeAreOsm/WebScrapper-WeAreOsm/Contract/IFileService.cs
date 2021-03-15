using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Scraper.Contracts
{
	interface IFileService
	{
		string CreateFileIfDoesNotExist(string path, string currency, string start, string end);

	}
}
