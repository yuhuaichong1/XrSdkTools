
namespace XrSDK
{
    //单例
    public abstract class SdkSingleton<T> where T : SdkILoad, new()
    {
        private static object obj = new object();
        private static T mInstance;

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (obj)
                    {
                        mInstance = new T();
                        mInstance.Load();
                    }
                }
                return mInstance;
            }
        }

        protected virtual void DestroyInstance()
        {
            mInstance = default;
        }
    }
}