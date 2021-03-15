Nacin pokretanja:
	1)	Aplikaciju mozemo pokrenuti pritiskom na f5 posto su podeseni defaultni ulazni argumenti za nju (2021-03-13 2021-03-15 EUR)
	2)	Mozemo je pokrenuti i preko cmd ili powerShell-a ali je onda potrebno da unesemo ulazne argumente
		Ulazni parametri su u sledecem formatu i moraju se uneti tacno definisanim redosledom:
		start: "2021-03-13"
		end: "2021-03-15"
		currency: "EUR"


Nacin implementacije:
	1)	Prvo  se kreira tekstualni fajl u koga cemo smestati podatke. 
		Podesavanje lokacije se nalazi u Startup.ExtractionPath
	
	2)	Zatim pozivamo GenerateCsv metodu ScrapDataServica
		U njoj imamo CountNumberOfPages metodu koja racuna koliko imamo stranica za zadati filter
		U zavisnosto koliko imamo stranica toliko cemo raditi get zahteva da bismo dovukli podatke za svaku stranicu
		U njoj se nalazi i GenerateCsvHeader za kreiranje hedera