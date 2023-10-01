namespace CloudStorage.Models
{
    public class CheckForNullPropertiesResult {
        public bool HasNullProperties { get; set; }
        public List<string>? NullProperties { get; set; }
    }
}