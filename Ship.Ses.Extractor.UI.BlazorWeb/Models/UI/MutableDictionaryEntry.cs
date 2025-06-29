namespace Ship.Ses.Extractor.UI.BlazorWeb.Models.UI
{
    // A generic mutable class to hold key-value pairs for UI binding
    public class MutableDictionaryEntry<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public MutableDictionaryEntry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    // Special entry for IdentifierTypeMap because its Value is a complex object
    public class MutableIdentifierTypeMapEntry
    {
        public string Key { get; set; } // The EMR Key (e.g., "nin")
        public IdentifierTypeMapEntry Value { get; set; } // The FHIR IdentifierTypeMapEntry object

        public MutableIdentifierTypeMapEntry(string key, IdentifierTypeMapEntry value)
        {
            Key = key;
            Value = value;
        }
    }
}
