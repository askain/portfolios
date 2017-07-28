using System.Runtime.Serialization;
using System.Web.Mvc;

namespace HDIMS.Models
{
    [DataContract]
    [ModelBinder(typeof(PutModelBinder))]
    public class PutModel<T>
    {
        [DataMember]
        public T Data { get; set; }
    }
}