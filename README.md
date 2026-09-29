# SupportWebApp

## Cosmos DB

Projektet bruger Azure Cosmos DB til at gemme supporthenvendelser.

### Oprettelse af Cosmos DB med Azure CLI

Opret først en resource group:

```bash
az group create \
  --name IBasSupportRG \
  --location westeurope

Opret derefter en Cosmos DB account:
az cosmosdb create \
  --name ibas-db-account-8989 \
  --resource-group IBasSupportRG

Opret databasen:
az cosmosdb sql database create \
  --account-name ibas-db-account-8989 \
  --resource-group IBasSupportRG \
  --name IBasSupportDB

Opret containeren:
az cosmosdb sql container create \
  --account-name ibas-db-account-8989 \
  --resource-group IBasSupportRG \
  --database-name IBasSupportDB \
  --name ibassupport \
  --partition-key-path "/category"

Connection strengen til Cosmos DB er gemt som en User Secret og ligger derfor ikke direkte i appsettings.json.

I appsettings.json ligger kun database- og container-navnet:
"CosmosDbSettings": {
  "DatabaseName": "IBasSupportDB",
  "ContainerName": "ibassupport"
}
Programmet henter connection stringen automatisk gennem configuration-systemet i Program.cs.


Status
Vi har nået følgende:
- Oprettet en model til supporthenvendelser
- Tilføjet validering med DataAnnotations
- Oprettet forbindelse til Azure Cosmos DB
- Oprettet en Cosmos DB-service
- Oprettet mulighed for at gemme nye supporthenvendelser
- Oprettet en Razor-side til oprettelse af henvendelser
- Oprettet en side der viser eksisterende henvendelser fra Cosmos DB
- Tilføjet siderne til navigationen
- Gemmer connection string sikkert med User Secrets
Hvad mangler
- Mulighed for at redigere henvendelser
- Mulighed for at slette henvendelser
- Bedre fejlhåndtering
- Eventuelt login og brugerroller
- Bedre design og brugeroplevelse
Næste trin
Næste trin vil være at udvide løsningen med fuld CRUD-funktionalitet, så supporthenvendelser kan oprettes, vises, redigeres og slettes.
```

