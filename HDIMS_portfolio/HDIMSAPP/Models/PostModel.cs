using System.Collections.Generic;

namespace HDIMSAPP.Models
{
    //[DataContract]
    //[ModelBinder(typeof(JsonModelBinder))]
    public class PostModel<T>
    {
        //[DataMember]
        public IList<T> Data { get; set; }
    }
}