using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Scraper.Contracts;

namespace Web_Scraper.Services
{
	class FileService : IFileService
	{
		public string CreateFileIfDoesNotExist(string path, string currency, string start, string end)
		{
			try
			{
				var fileName = CreateFileName(path, currency, start, end);

				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}
				File.Create(fileName).Close();
				return fileName;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}



		}

		public string CreateFileName(string path, string currency, string start, string end)
			=> path + @"\" + $"{currency}_{start}_{end}.txt";
	}
}
