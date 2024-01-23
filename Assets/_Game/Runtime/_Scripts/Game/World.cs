using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class World : SceneSingleton<World>
    {
        [SerializeField] private Parallax[] parallaxs;
        public Parallax[] Parallaxs => parallaxs;
    }
}
