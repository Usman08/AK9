using AK9.AppHelper.Enums;

namespace AK9.AppHelper.Models
{
    public struct StatusModel
    {
        public StatusEnum TransactionStatus { get; set; }
        public string StatusMessage { get; set; }
    }
}
