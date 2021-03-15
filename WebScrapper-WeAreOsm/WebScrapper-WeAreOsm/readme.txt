Nacin pokretanja:
	1) Pre pokretanja aplikacije potrebno je restorovati nuget pakete
	2) Aplikaciju mozemo pokrenuti iz visual studija ali je potrebno uneti command line arguments u debug sesiji.
	3) Aplikaciju mozemo pokrenuti i preko cmd ili poverShell-a ali je takodje potrbno uneti ulazne parametre.
	4) Ulaznu paramatri su npr:
		2021-03-13 2021-03-15 EUR
		i oznacavaju redom start date, end data i currency


Nacin implementacije:
	1)	Prvo  se kreira tekstualni fajl u koga cemo smestati podatke. 
		Podesavanje lokacije se nalazi u Startup.ExtractionPath
	
	2)	Zatim pozivamo GenerateCsv metodu ScrapDataServica
		U njoj imamo CountNumberOfPages metodu koja racuna koliko imamo stranica za zadati filter
		U zavisnosto koliko imamo stranica toliko cemo raditi get zahteva da bismo dovukli podatke za svaku stranicu
		U njoj se nalazi i GenerateCsvHeader za kreiranje hedera