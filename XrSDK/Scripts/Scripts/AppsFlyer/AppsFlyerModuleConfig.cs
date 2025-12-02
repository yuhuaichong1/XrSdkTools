//using AppsFlyerSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrSDK
{
    //AppsFlyer 模块挂件
    [RegisterModule("AppsFlyer Module")]
    public class AppsFlyerModuleConfig : BaseModulePendant
    {
        public string devKey = "";
        public string appID = "";
        public string UWPAppID = "";
        public string macOSAppID = "";
        public bool isDebug = false;
        public bool getConversionData = false;
        public AppsFlyerModuleConfig()
        {
            moduleName = "AppsFlyer";
        }

        public override void CreateModule()
        {
            AppsFlyerData data = new AppsFlyerData();
            data.devKey = devKey;
            data.appID = appID;
            data.UWPAppID = UWPAppID;
            data.macOSAppID = macOSAppID;
            data.isDebug = isDebug;
            data.getConversionData = false;     //因为SDK插件模式限制，暂不支持数据转换 TODO：MonoInst中转方式是否可行？
#if UNITYEDITOR
            return;
#endif
            AppsFlyerModule module = new AppsFlyerModule(data);
            module.Load();
        }
    }

}