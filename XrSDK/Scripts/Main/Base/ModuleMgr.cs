using System.Collections.Generic;

namespace XrSDK
{
    public class ModuleMgr : SdkSingleton<ModuleMgr>, SdkILoad, SdkIDispose
    {
        public List<SdkBaseModule> updateModList;

        private bool isLoaded = false;
        public void Load()
        {
            updateModList = new List<SdkBaseModule>();
        }
        public void Dispose()
        {
            isLoaded = false; 
        }

        public void Start()
        {
            isLoaded = true;
        }

        public void Update()
        {
            if (isLoaded)
            {
                //AssetBundleMod.Instance.Update();
                for (int i = 0; i < updateModList.Count; i++)
                {
                    updateModList[i].Update();
                }

                //UIManager.Instance.Update();
            }
        }

        public void FixedUpdate()
        {
            if (isLoaded)
            {
                //AssetBundleMod.Instance.Update();
                for (int i = 0; i < updateModList.Count; i++)
                {
                    updateModList[i].FixedUpdate();
                }
            }
        }

        //注册更新模块
        public void RegistUpdateObj(SdkBaseModule module)
        {
            updateModList.Add(module);
        }
    }
}