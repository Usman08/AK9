using AK9.AppHelper.AppConstants;
using AK9.AppHelper.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace AK9.AppHelper.Utils
{
    public static class StatusTempData
    {
        public static StatusModel GetStatus(this ITempDataDictionary tempData)
        {
            return DeserializeStatus(tempData[HelpingVariable.STATUS] as string);
        }

        public static void SetStatus(this ITempDataDictionary tempData, StatusModel status)
        {
            tempData[HelpingVariable.STATUS] = SerializeStatus(status);
        }

        private static string SerializeStatus(StatusModel tempData)
        {
            return JsonConvert.SerializeObject(tempData);
        }

        private static StatusModel DeserializeStatus(string tempData)
        {
            return JsonConvert.DeserializeObject<StatusModel>(tempData);
        }
    }
}
