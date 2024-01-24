using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird
{
    public static class Config
    {
        public static int fpsTarget = 60;
    }

    public enum EParallaxType
    {
        Slow,
        HalfMedium,
        Medium,
        Normal,
    }

    public enum EGameState
    {
        Start,
        Play,
        GameOver,
    }

    public enum EAudioType
    { 
        Hit,
        Point,
        Flap,
    }
}
