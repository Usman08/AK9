using System.Collections.Generic;

namespace AK9.AppHelper.Models
{
    public class ServiceListModel : BaseModel
    {
        public ServiceSearchModel SearchModel { get; set; }
        public List<ServiceModel> ServiceList { get; set; }
    }
}
