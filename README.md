## Zadanie - Lata przestępne na bazie danych

Zmodyfikuj aplikację webową "Lata przestępne" w taki sposób, aby:



- [x] wyszukiwanie było dostępne dla wszystkich użytkowników (zalogowanych i niezalogowanych). Do uwierzytelniania użyj modułu ClaimsIdentity
- [x] pole imię powinno być opcjonalne (0,25pkt)
- [x] dowolny użytkownik może sprawdzić dowolny rok, czy jest przestępny czy nie. Każde zwalidowane wyszukanie (rok, data i godzina wyszukania oraz wynik) zostało zapisane w bazie danych (0,5pkt)
- [x] aplikacja powinna mieć dodatkową stronę "Historia wyszukiwań", na której będzie wyświetlana lista wszystkich wyszukiwań posortowanych malejąco według czasu. Jeśli liczba elementów w liście przekracza 20, na stronie powinno pojawić się stronicowanie po 20 wpisów na stronę - https://learn.microsoft.com/pl-pl/aspnet/core/data/ef-rp/sort-filter-page?view=aspnetcore-6.0#add-paging (1pkt)
- [x] na stronie Historia wyszukiwań powinien być wyświetlony login i ID użytkownika, który sprawdzał przestępność roku (jeśli rok był wyszukiwany przez zalogowanego użytkownika) (0,5pkt)
- [x] aplikacja powinna udostępnić dla zalogowanych użytkowników możliwość usunięcia wpisu z historii (jeśli rok został wyszukany przez aktualnie zalogowanego użytkownika) (0,5pkt), 
  - [x] edycja historycznych wpisów nie powinna być dostępna dla żadnego użytkownika (0.25pkt) (*Jest co prawda dostępna ale edycja nie zmieni wpisu ;)*)

## What's done?

- [x] CRUD for YearNameForm

- [x] Authentication and authorization


