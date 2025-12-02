using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XrCode;


namespace XrSDK
{
    public class AppsFlyerModule : BaseModule
    {
        public string devKey;                                  //qMMYrLguTV9WjiFdtnk4rA
        public string appID;
        public string UWPAppID;
        public string macOSAppID;
        public bool isDebug;
        public bool getConversionData;

        public AppsFlyerModule(AppsFlyerData data)
        {
            if (data != null)
            {
                devKey = data.devKey;
                appID = data.appID;
                UWPAppID = data.UWPAppID;
                macOSAppID = data.macOSAppID;
                isDebug = data.isDebug;
                getConversionData = data.getConversionData;
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            //AddEvent();
            // ToDo?
            //AppsFlyer.enableTCFDataCollection(true);
                Guid guid = Guid.NewGuid();
                string guidString = guid.ToString();
//                AppsFlyer.setCustomerUserId(guidString);

//                // These fields are set from the editor so do not modify!
//                //******************************//
//                AppsFlyer.setIsDebug(isDebug);
//#if UNITY_WSA_10_0 && !UNITY_EDITOR
//        AppsFlyer.initSDK(devKey, UWPAppID, getConversionData ? MonoInst.Instance : null);
//#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
//    AppsFlyer.initSDK(devKey, macOSAppID, getConversionData ? MonoInst.Instance : null);
//#else
//                AppsFlyer.initSDK(devKey, appID, getConversionData ? MonoInst.Instance : null);
//#endif
//                //******************************/CONTENT_TYPE

//                AppsFlyer.startSDK();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            //RemoveEvent();
        }

        private void AddEvent()
        {
            AppsFlyerDefines.SendAdSuccessCountEvent = SendAdSuccessCountEvent;
            //AppsFlyerDefines.SendAdRevenue = SendAdRevenue;
        }

        private void RemoveEvent()
        {
            AppsFlyerDefines.SendAdSuccessCountEvent = null;
            //AppsFlyerDefines.SendAdRevenue = null;
        }

        //广告成功播放接口
        private void SendAdSuccessCountEvent(int adCount)
        {
            //D.Error(" ____________________________________________ 12356");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("adCount", adCount.ToString());
            //AppsFlyer.sendEvent("AdSuccessEvent", dic);
        }

        //广告收入
        //private void SendAdRevenue(MaxSdkBase.AdInfo adInfo, EAdType eAdType)
        //{
        //    D.Error(" ____________________________________________ 123567");
        //    string adType = GetAdType(eAdType);
        //    Dictionary<string, string> additionalParams = new Dictionary<string, string>();
        //    //additionalParams.Add(AdRevenueScheme.COUNTRY, adInfo.WaterfallInfo);
        //    additionalParams.Add(AdRevenueScheme.AD_UNIT, adInfo.AdUnitIdentifier);
        //    additionalParams.Add(AdRevenueScheme.AD_TYPE, adType);
        //    additionalParams.Add(AdRevenueScheme.PLACEMENT, adInfo.Placement);
        //    var logRevenue = new AFAdRevenueData("monetizationNetworkEx", MediationNetwork.ApplovinMax, "USD", adInfo.Revenue);
        //    AppsFlyer.logAdRevenue(logRevenue, additionalParams);
        //}

        private string GetAdType(EAdType eAdType)
        {
            switch (eAdType)
            {
                case EAdType.EReward:
                    return "reward";
                case EAdType.EInterstitial:
                    return "interstitial";
                case EAdType.EBanner:
                    return "banner";
                case EAdType.ERec:
                    return "rec";
            }
            return "";
        }

        // Mark AppsFlyer CallBacks
        public void onConversionDataSuccess(string conversionData)
        {
            //AppsFlyer.AFLog("didReceiveConversionData", conversionData);
            //Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);
            // add deferred deeplink logic here
        }

        public void loadConversionData(string conversionData) 
        { 
        
        }

        public void onConversionDataFail(string error)
        {
            //AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
        }

        //当应用打开时的归因处理
        public void onAppOpenAttribution(string attributionData)
        {
            //AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
            //Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
            // add direct deeplink logic here
        }

        //当应用打开时归因失败
        public void onAppOpenAttributionFailure(string error)
        {
            //AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
        }
    }
}