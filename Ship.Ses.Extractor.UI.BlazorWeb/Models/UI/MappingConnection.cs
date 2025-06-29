namespace Ship.Ses.Extractor.UI.BlazorWeb.Models.UI
{
    // Represents a simple visual link or a basic mapping between an EMR source and FHIR target
    public class MappingConnection
    {
        public string SourceNodeId { get; set; } // ID of the EMR table/column (from MappingNode.Id)
        public string TargetNodeId { get; set; } // ID of the FHIR field (from MappingNode.Id)
        public string SourceLabel { get; set; }
        public string TargetLabel { get; set; }
        public string FhirPath { get; set; } // The full FHIR path for the target
        public string EmrField { get; set; } // The EMR Table.Column or just Column name
        public string EmrTable { get; set; } // The EMR Table name
        public string DataType { get; set; } // Suggested data type based on EMR/FHIR (can be refined in modal)

        // Additional properties to represent the complex mapping,
        // which will be populated and edited via the modal
        public FieldMappingConfigurationModel Configuration { get; set; }

        public MappingConnection(MappingNode source, MappingNode target)
        {
            if (source == null || target == null || source.Type != "EMR" || target.Type != "FHIR")
            {
                throw new ArgumentException("MappingConnection requires an EMR source node and a FHIR target node.");
            }

            SourceNodeId = source.Id;
            TargetNodeId = target.Id;
            SourceLabel = source.Label;
            TargetLabel = target.Label;
            FhirPath = target.Path;
            EmrTable = source.TableName;
            EmrField = source.ColumnName; // If source is a column, otherwise leave null or derive from TableName
            DataType = target.DataType; // Initial suggested data type from FHIR target

            // Initialize configuration with basic values from the connection
            Configuration = new FieldMappingConfigurationModel
            {
                FhirPath = target.Path,
                EmrField = source.ColumnName, // If mapping a specific column
                DataType = target.DataType, // Initial type from FHIR
                Required = target.IsRequired // Initial required state from FHIR
            };

            // If the source is just a table, the emrField might be null initially.
            // It will be fully defined when the user opens the configuration modal.
            if (!string.IsNullOrEmpty(source.ColumnName))
            {
                Configuration.EmrField = source.ColumnName;
            }
            else // If user selects a table first, they'll specify the column in the modal
            {
                Configuration.EmrField = null; // Or a placeholder
                Configuration.EmrFieldMap = new Dictionary<string, string>(); // Initialize for complex mappings
            }
        }
    }
}
