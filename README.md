🏆 Tournament API – Dokumentation  
Detta är ett ASP.NET Core Web API-projekt uppdelat i flera delar enligt principen Separation of Concerns:

📁 Projektstruktur  
- **Tournament.Api** – Extensions och Program.cs  
- **Tournament.Presentation** – Controllers
- **Tournament.Core** – DTOs, QueryParameters  
- **Tournament.Data** – Databasåtkomst, repository, Unit of Work
- **Tournament.Services** – Services
- **Service.Contracts** – Interfaces för tjänster  
- **Domain.Contracts** – Interfaces för repositories  
- **Domain.Models** – Entiteter och modeller  

---

🎯 Funktioner  

🏅 **Turneringar (Tournaments)**  
- Hämta alla turneringar med valbara parametrar:  
  - `includeGames` (bool) – om matcher ska inkluderas  
  - `title` (string) – filtrering på titel  
  - `pageNumber` (int) – sidnummer för pagination *(standard: 1)*  
  - `pageSize` (int) – antal objekt per sida *(standard: 10)*  
- Hämta en turnering efter ID, med eller utan matcher  
- Skapa, uppdatera *(PUT och PATCH)* och ta bort en turnering och dess matcher  
- 📄 **Pagination** – Stöd för att hämta data i mindre delar med metadata (`TotalCount`, `Items`)  

🎮 **Matcher (Games)**  
- Hämta alla matcher, filtrera på titel eller tid  
- Hämta en match efter ID eller titel  
- Skapa, uppdatera och ta bort matcher  

---

🧪 **Testdata & Seedning**  
- 🤖 **Bogus** används för att generera realistiska turneringar och matcher vid uppstart  
- Möjlighet att utöka seedningen med egna scenarion  

---

🛠 **Teknisk info**  
- Entity Framework Core används för databasinteraktion  
- Repository Pattern + Unit of Work för abstraktion och transaktionshantering  
- AutoMapper för att mappa entiteter till DTOs och vice versa  
- Asynkrona metoder används konsekvent för bättre prestanda  
- Felhantering med lämpliga HTTP-statuskoder och tydliga felmeddelanden  
- Validering av inkommande data *(t.ex. titel måste finnas)*  
- Pagination via `pageNumber` och `pageSize`  

---

📝 **Att göra**   
- Skapa ett testprojekt och skriv enhetstester för kontrollerna och deras metoder.  
