using System.Diagnostics;
using PlatfromTools.Ads;
using PlatfromTools.Controllers;
using PlatfromTools.GameServices;
using PlatfromTools.ScreenInfos;

namespace PlatfromTools.Platforms
{
    public class EditorPlatform : AbstractPlatform
    {

        protected override AbstractController[] InitControllers()
        {
            return new AbstractController[] {new AndroidController() ,  new MouseController() };
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