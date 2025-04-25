# SHIP FHIR Extractor & Mapper

The **SHIP FHIR Extractor** is a domain-driven .NET tool for mapping and transforming EMR (Electronic Medical Record) data into HL7 FHIR-compliant JSON using dynamic field mapping, validation, and persistence logic. It’s part of the Lagos State Smart Health Information Platform (SHIP) and enables healthcare data integration at scale.

---

## 🔧 Architecture

This solution follows clean, layered **Domain-Driven Design (DDD)** with the following core projects:

- `Ship.Ses.Extractor.Domain` – Core domain models and logic
- `Ship.Ses.Extractor.Application` – Service contracts and use cases
- `Ship.Ses.Extractor.Infrastructure` – Persistence, transformation, MongoDB + EF Core access
- `Ship.Ses.Extractor.Presentation.Api` – Web API for managing mappings and EMR integration
- `Ship.Ses.Extractor.UI.BlazorWeb` – Admin UI for visual field mapping
- `Ship.Ses.Extractor.Worker` – Background service for periodic extraction and persistence

---

## ✨ Key Features

### 🧠 Blazor Web UI
- Admin selects a **FHIR Resource Type** (e.g. `Patient`, `Encounter`) from a dropdown.
- System connects to a configured EMR (MySQL, PostgreSQL, or MSSQL) and loads table/column metadata.
- Admin visually maps:
  - 🔹 Left Panel: EMR tables and fields  
  - 🔸 Right Panel: FHIR JSONPath / FHIRPath-like structure
- Mappings can be **saved**, **edited**, or **exported as JSON**.

### ⚙️ Mapping Engine
- Dynamically converts SQL rows to FHIR resource JSON using field mappings
- Supports **constants injection** (e.g. `relationship`, `identifier.type`, `managingOrganization`)
- Validates generated JSON against a FHIR schema engine
- Persists to MongoDB collection per resource type
- Includes support for **retry** on transform/persist failure

### 📦 Persistence & Pipeline Ready
- Uses **Entity Framework Core** to persist EMR connection info and mapping metadata
- MongoDB for storing FHIR records with custom retry, status, and sync tracking
- Logging powered by **Serilog**, enriched with environment + correlation ID

---

## 📘 API Capabilities

Hosted in `Ship.Ses.Extractor.Presentation.Api`

| Endpoint | Description |
|----------|-------------|
| `GET /api/emr/tables` | List tables from selected EMR DB |
| `GET /api/emr/tables/{name}` | Inspect columns in table |
| `POST /api/emr/connections/select/{id}` | Select EMR data source |
| `GET /api/mappings` | List saved mappings |
| `GET /api/mappings/{id}` | View mapping |
| `POST /api/mappings` | Create mapping |
| `PUT /api/mappings/{id}` | Update mapping |
| `DELETE /api/mappings/{id}` | Delete mapping |

### ✅ Swagger is enabled at:
