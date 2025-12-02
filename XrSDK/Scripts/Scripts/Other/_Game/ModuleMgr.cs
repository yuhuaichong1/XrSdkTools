using System.Collections.Generic;

namespace XrCode
{
    public class ModuleMgr : Singleton<ModuleMgr>, ILoad, IDispose
    {
        public List<BaseModule> updateModList;

        private bool isLoaded = false;
        public void Load()
        {
            updateModList = new List<BaseModule>();
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
        public void RegistUpdateObj(BaseModule module)
        {
            updateModList.Add(module);
        }
    }
}