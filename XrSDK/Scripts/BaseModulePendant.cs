using UnityEngine;

namespace XrSDK
{
    //模块挂件基类
    public abstract class BaseModulePendant : ScriptableObject
    {
        [HideInInspector]
        [SerializeField]
        protected string moduleName;
        //运行时创建模块
        public abstract void CreateModule();
    }
}