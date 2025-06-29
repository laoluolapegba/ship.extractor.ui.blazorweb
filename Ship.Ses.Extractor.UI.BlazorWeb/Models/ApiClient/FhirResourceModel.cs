namespace Ship.Ses.Extractor.UI.BlazorWeb.Models.ApiClient
{
    public class FhirResourceTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Structure { get; set; }
    }

    public class FhirFieldModel
    {
        public string Path { get; set; } // e.g., "Patient.name[0].given"
        public string DisplayName { get; set; } // e.g., "given"
        public string DataType { get; set; } // e.g., "String", "Object", "Array", "Quantity"
        public bool IsRequired { get; set; }
        public List<FhirFieldModel> Children { get; set; } = new List<FhirFieldModel>();

        // Optional: Could add a property for FHIR template names if applicable,
        // e.g., for "name", "address", "telecom"
        public string TemplateName { get; set; }
    }
}
