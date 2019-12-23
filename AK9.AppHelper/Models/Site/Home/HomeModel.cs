using AK9.AppHelper.Utils;
using System.Collections.Generic;

namespace AK9.AppHelper.Models
{
    public class HomeModel
    {
        public string CertificationLogoPath { get; set; }
        public AppSettings AppSettings { get; set; }
        public List<ServiceModel> ServiceList { get; set; }
        public List<CertificationModel> CertificationList { get; set; }
        public AskAQuoteModel AskAQuote { get; set; }
    }
}
