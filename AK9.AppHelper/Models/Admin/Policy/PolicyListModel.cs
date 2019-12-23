using System.Collections.Generic;

namespace AK9.AppHelper.Models
{
    public class PolicyListModel : BaseModel
    {
        public PolicySearchModel SearchModel { get; set; }
        public List<PolicyModel> PolicyList { get; set; }
    }
}
