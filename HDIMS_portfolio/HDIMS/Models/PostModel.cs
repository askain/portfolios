using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace HDIMS.Models
{
    [DataContract]
    [ModelBinder(typeof(JsonModelBinder))]
    public class PostModel<T>
    {
        [DataMember]
        public IList<T> Data { get; set; }
    }
}