using System;
using PlatfromTools.Ads;
using PlatfromTools.Controllers;
using PlatfromTools.GameServices;
using PlatfromTools.ScreenInfos;

namespace PlatfromTools.Platforms
{
    public abstract class AbstractPlatform
    {
        protected AbstractController[] Controllers;

        protected AbstractPlatform()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Controllers = InitControllers();
        }

        protected abstract AbstractController[] InitControllers();

        public virtual AbstractController GetController()
        {
            AbstractController result = null;
            if (Controllers == null&&Controllers.Length>0)
                throw new Exception("Controllers should be initialized to be used");
            for (int i = 0; i < Controllers.Length; i++)
            {
                if (Controllers[i].IsActive())
                {
                    
                    result = Controllers[i];
                    break;
                }
            }
            return result;
        }
        public abstract AbstractScreenInfo GetScreenInfo();
        public abstract AbstractAds GetAds();
        public abstract AbstractGameService GetGameService();


    }
}