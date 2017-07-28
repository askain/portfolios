using System;
using System.Collections.Generic;

namespace HDIMSMAIN.Models
{
    //[DataContract]
    //[ModelBinder(typeof(JsonModelBinder))]
    public class PostModel<T>
    {
        //[DataMember]
        public IList<T> Data { get; set; }
    }
}