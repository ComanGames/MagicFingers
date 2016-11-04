using PlatfromTools.Ads;
using PlatfromTools.Controllers;
using PlatfromTools.GameServices;
using PlatfromTools.ScreenInfos;
using UnityEngine;

namespace PlatfromTools.Platforms
{
    public class AndroidPlatform:AbstractPlatform
    {
        protected override AbstractController[] InitControllers()
        {
            return new AbstractController[] {new AndroidController()};
        }

        public override AbstractScreenInfo GetScreenInfo()
        {
            throw new System.NotImplementedException();
        }

        public override AbstractAds GetAds()
        {
            throw new System.NotImplementedException();
        }

        public override AbstractGameService GetGameService()
        {
            throw new System.NotImplementedException();
        }

    }
}