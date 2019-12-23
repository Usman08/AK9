using AK9.AppHelper.Enums;

namespace AK9.AppHelper.Models
{
    public abstract class BaseSearchModel
    {
        public string SortColumn { get; set; }
        public SortOrderEnum SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
