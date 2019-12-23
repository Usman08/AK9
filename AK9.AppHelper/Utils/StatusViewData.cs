using AK9.AppHelper.AppConstants;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace AK9.AppHelper.Utils
{
    public static class StatusViewData
    {
        public static T GetViewData<T>(this ViewDataDictionary tempData, string viewDataName)
        {
            return DeserializeStatus<T>(tempData[viewDataName] as string);
        }

        public static void SetViewData<T>(this ViewDataDictionary tempData, T status, string viewDataName)
        {
            tempData[viewDataName] = SerializeStatus(status);
        }

        private static string SerializeStatus<T>(T viewData)
        {
            return JsonConvert.SerializeObject(viewData);
        }

        private static T DeserializeStatus<T>(string viewData)
        {
            return JsonConvert.DeserializeObject<T>(viewData);
        }
    }
}
