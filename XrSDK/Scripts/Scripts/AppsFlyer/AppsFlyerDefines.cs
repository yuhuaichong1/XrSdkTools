//using AppLovinMax;
using System;

namespace XrSDK
{
    public static class AppsFlyerDefines
    {
        public static Action<int> SendAdSuccessCountEvent;                                   //发送广告完播事件
        //public static Action<MaxSdkBase.AdInfo, EAdType> SendAdRevenue;                    //发送广告收益事件
    }

    public enum EAdType
    {
        EReward = 1,
        EInterstitial = 2,
        EBanner = 3,
        ERec = 4,
    }
}