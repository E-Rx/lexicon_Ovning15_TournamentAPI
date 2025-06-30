
# 🏆 Tournament API – Dokumentation

Detta är ett ASP.NET Core Web API-projekt uppdelat i tre delar:

* **Tournament.Api** – Controllers och API-endpoints
* **Tournament.Core** – Domänmodeller, DTOs, interfaces och QueryParameters.
* **Tournament.Data** – Databasåtkomst, repository, Unit of Work och AutoMapper

## 🎯 Funktioner

### 🏅 Turneringar (Tournaments)

* Hämta alla turneringar med valbara parametrar:
   - includeGames (bool) – om matcher ska inkluderas
   - title (string) – filtrering på titel
   - pageNumber (int) – sidnummer för pagination (standard: 1)
   - pageSize (int) – antal objekt per sida (standard: 10)
* Hämta en turnering efter id, med eller utan matcher
* Skapa, uppdatera en turnering (PUT och PATCH) och ta bort en turnering och dess matcher

### 🎮 Matcher (Games)

* Hämta alla matcher, filtrera på titel eller Tid
* Hämta en match efter eller titel
* Skapa, uppdatera och ta bort matcher

## 🛠 Teknisk info

* Entity Framework Core används för databasinteraktion
* Repository Pattern + Unit of Work för abstraktion och transaktionshantering
* AutoMapper för att mappa entiteter till DTOs och vice versa
* Asynkrona metoder används konsekvent för bättre prestanda
* Felhantering med lämpliga HTTP-statuskoder och tydliga felmeddelanden
* Validering av inkommande data (t.ex. titel måste finnas)
* Pagination stöds via pageNumber och pageSize för att effektivt hämta data i mindre delar

## 📝 Att göra

* Säkerställ att matchernas datum i SeedData inte överlappar och att de ligger inom turneringens tidsperiod. Skriv separata metoder för detta i SeedData.
* Skapa ett testprojekt och skriv enhetstester för kontrollerna och deras metoder.

----------------------------------------------------

