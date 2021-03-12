# rp Accounting
[![CI](https://github.com/pirren/rp.Accounting/actions/workflows/CI.yml/badge.svg)](https://github.com/pirren/rp.Accounting/actions/workflows/CI.yml)

## Om projektet 
rp.Accounting är en bokföringsapplikation för ett småföretag, byggt i Blazor Desktop miljö. Appen använder ett integrerat API för dataöverföring och SQLite för datalagring. Den är cross-platform via ElectronNET skal. För mig är detta ett pilotprojekt för Domän-driven design.

Målet för användaren är att spara tid genom autogenererade formulär och en kontinuerligt uppdaterad kunddatabas.

## Användning
Användaren vill ha autogenererade formulär för bokföringsunderlag. Det finns olika typer av formulär. Varje månad genereras ett nytt formulär upp och baseras på kundregistret, och alla formulär sparas. 

Det finns möjlighet att exportera formulären till excelfiler.



*Lathund, kompileringskommandon för ElectronNET:*
```
electronize init
electronize start
electronize build /target win
```

