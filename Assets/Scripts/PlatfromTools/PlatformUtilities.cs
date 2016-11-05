using System;
using PlatfromTools.Controllers;
using PlatfromTools.Platforms;

namespace PlatfromTools
{
    public class PlatformUtilities
    {

        #region static-part

        public static PlatformUtilities RealInstance;

        private static PlatformUtilities Instance
        {
            get
            {
                if(RealInstance==null)
                    return new PlatformUtilities();
                return RealInstance;
            }
        }


        public static AbstractController GetController()
        {
            return Instance._platform.GetController();
        }
        

        #endregion
        #region non-staticPart

        private AbstractPlatform _platform;
        public PlatformUtilities()
        {
            _platform = InitPlatform();
            RealInstance = this;
        }

        private AbstractPlatform InitPlatform()
        {

#if UNITY_EDITOR||UNITY_EDITOR_64
            return new EditorPlatform();
#endif

#if UNITY_ANDROID 
            return new AndroidPlatform();
#endif
            throw new Exception("Not supported platform");
        }

        #endregion

       }
}