using AK9.AppHelper.Utils;
using System.Collections.Generic;

namespace AK9.AppHelper.Models
{
    public class ServiceDetailModel
    {
        public string ServiceBannerPath { get; set; }
        public AppSettings AppSettings { get; set; }
        public ServiceModel Service { get; set; }
        public List<ServiceModel> ServiceList { get; set; }
    }
}
