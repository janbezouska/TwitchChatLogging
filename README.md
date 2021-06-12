# TwitchChatLogger
### Specifikace požadavků - Verze 1
Jan Bezouška  
bezouska.jan.2018@skola.ssps.cz  
16.5.2021  

- Úvod
  - Konvence dokumentu
    - Důležitost - 1 největší
  - Kontakty
    - Jan Bezouška, bezouska.jan.2018@skola.ssps.cz
- Celkový popis
  - Program bude napojený na twitch API
  - Program bude zapisovat zprávy a informace o nich do databáze
  - Program není určený pro uživatele, jen pro mě
  - Program bude závislý na TwitchLib - https://github.com/TwitchLib/TwitchLib
- Vlastnosti systému  
  1. Zapisování do databáze
    - Důležitost: 1
    - Vstup: informace z API o nové zprávě
    - Akce: zapsání zprávy a informací o ní do databáze
- Nefunkční požadavky
  - Odezva
    - Zapisování do databáze by mělo trvat max. 1 vteřinu
  - Bezpečnost
    - Heslo a token bota který bude připojený na twitch bude v configu na mém počítači a budou dostupné pouze mně

### Funkční specifikace - Verze 2
Jan Bezouška  
bezouska.jan.2018@skola.ssps.cz  
16.5.2021  

- Úvod
    - Dokument je určený pro mě
- Scénáře
    - Způsoby použití
        - Tento program se nebude "používat" - pouze konstantně poběží a bude zapisovat zprávy do databáze (dále "db")
    - Uživatelské role
        - Já - budu jediný, kdo bude mít k programu přístup
- Architektura
    - Pracovní tok
        - Někdo napíše něco do chatu
        - Program dostane informaci o dané zprávě
        - Program si uloží dané informace do třídy
        - Pošle uložené informace do databáze
            - Možný error: nepovedlo se zapsat do db
    - Další errory
        - Program přišel o spojení s Twitch API
    - Uživatelské rozhraní
        - Konzolová aplikace
        - Bude se vypisovat, zda se povedlo zapsat data do db



