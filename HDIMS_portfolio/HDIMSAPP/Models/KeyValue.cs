
namespace HDIMSAPP.Models
{
    public class KeyValue<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public bool Selected { get; set; }
    }
}
