# Kreipiniai

Pateikiama biblioteka skirta taisyklingai naudoti lietuviškų vardų kreipinius šauksmininko linksniu. Biblioteka sugeba rasti informaciją pagal http://vardai.vlkk.lt/ puslapį, pasitikrinti, ar toks vardas egzistuoja, ir jei taip - teisingai grąžinti vardą šauksmininko linksniu, arba jei ne - grąžinti tą patį žodį.

## Naudojimas

Biblioteka naudoja `MemoryCache` siekiant sumažinti į serverį siunčiamų užklausų skaičių, todėl taisyklingai ją naudojant reikėtų ją inicializuoti tik vieną kartą.

`var kreipiniai = new Kreipiniai();`

Kaip konstruktoriaus parametrą galima paduoti `TimeSpan` nurodantį kiek laiko įrašai bus saugomi `MemoryCache`. Pagal nutylėjimą šis laikas yra 30 dienų.

`var kreipiniai = new Kreipiniai(TimeSpan.FromDays(30));`

Toliau kreipiniai (šauksmininko linksniu) gaunami kviečiant `GetFor(string name)` funkciją:

`var sauksmininkas = kreipiniai.GetFor("Ramūnas');`
