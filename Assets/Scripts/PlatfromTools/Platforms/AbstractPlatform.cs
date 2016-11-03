using System;
using PlatfromTools.Ads;
using PlatfromTools.Controllers;
using PlatfromTools.GameServices;
using PlatfromTools.ScreenInfos;
using UnityEngine;

namespace PlatfromTools.Platforms
{
    public abstract class AbstractPlatform
    {
        protected Camera GameCamera;
        protected float ZDistance;
        protected AbstractController[] Controllers;

        protected AbstractPlatform(Camera gameCamera, float zDistance)
        {
            GameCamera = gameCamera;
            ZDistance = zDistance;
            // ReSharper disable once VirtualMemberCallInConstructor
            Controllers = InitControllers();
        }

        protected abstract AbstractController[] InitControllers();

        public virtual AbstractController GetController()
        {
            AbstractController result = null;
            if (Controllers == null)
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