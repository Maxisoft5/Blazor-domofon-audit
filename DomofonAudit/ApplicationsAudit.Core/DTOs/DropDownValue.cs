namespace ApplicationsAudit.Core.DTOs
{
    public class DropDownValue<T> where T : struct
    {
        public string Label { get; set; }
        public T Value { get; set; }
    }
}
