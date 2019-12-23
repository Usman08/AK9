using System.Collections.Generic;

namespace AK9.AppHelper.Models
{
    public class CertificationListModel : BaseModel
    {
        public CertificationSearchModel SearchModel { get; set; }
        public List<CertificationModel> CertificationList { get; set; }
    }
}
