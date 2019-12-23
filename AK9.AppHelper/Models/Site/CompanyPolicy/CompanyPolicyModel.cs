using AK9.AppHelper.Utils;
using System.Collections.Generic;

namespace AK9.AppHelper.Models
{
    public class CompanyPolicyModel
    {
        public string PolicyPdfPath { get; set; }
        public AppSettings AppSettings { get; set; }
        public List<PolicyModel> PolicyList { get; set; }
    }
}
