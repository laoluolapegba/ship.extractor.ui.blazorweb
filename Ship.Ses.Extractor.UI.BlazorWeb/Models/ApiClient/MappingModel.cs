using Ship.Ses.Extractor.UI.BlazorWeb.Models.UI;

namespace Ship.Ses.Extractor.UI.BlazorWeb.Models.ApiClient
{
    public class MappingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FhirResourceTypeId { get; set; }
        public string FhirResourceTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public List<FieldMappingConfigurationModel> Mappings { get; set; } = new List<FieldMappingConfigurationModel>();
    }

    public class ColumnMappingModel
    {
        public string EmrTable { get; set; }
        public string EmrColumn { get; set; }
        public string FhirPath { get; set; }
        public string TransformationExpression { get; set; }
    }
}
