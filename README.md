SHIP FHIR Mapper Web UI

This project is a Blazor Web UI component of the SHIP Extractor platform, used to map EMR database tables and fields to FHIR-compliant JSON structures.

It empowers technical admins to create, manage, and export field mappings that define how EMR data is transformed into FHIR resource representations.

✨ Key Features

🔽 Select FHIR Resource: Choose a resource type like Patient, Encounter, etc.

🔄 Live EMR Metadata: Connects to the configured EMR database and loads tables & columns.

🧭 Drag & Drop Mapping:

Left panel: EMR tables and fields

Right panel: FHIR structure (JSONPath/FHIRPath)

💾 Mapping Management:

Create / Edit mappings

Export mapping as JSON

Save to central API

🛠️ Project Structure

This Web UI runs as part of the Ship.Ses.Extractor.UI.BlazorWeb project in the SHIP Extractor solution.

It communicates with the backend API (Ship.Ses.Extractor.Presentation.Api) to fetch:

Active EMR connections

Table metadata

Saved mapping definitions

Supported FHIR resource types

🔧 Configuration

Set the base API endpoint in appsettings.json:

{
  "ApiBaseUrl": "https://localhost:7015/api/v1/"
}

🚀 Getting Started

dotnet run --project Ship.Ses.Extractor.UI.BlazorWeb

Then navigate to https://localhost:7016/ (or your configured port).

Ensure the API project is also running to provide data.

🧱 Supported Resource Types

The UI supports mapping for FHIR resource types defined in the backend, such as:

Patient

Encounter

Observation

These can be extended in the backend.

🔒 Authentication / Security

This version assumes the tool is used by internal admins and does not require login. In future versions, integration with Azure AD or SHIP IAM can be supported.

🏷️ License



