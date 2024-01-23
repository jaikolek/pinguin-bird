using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private EParallaxType parallaxType;
        public EParallaxType ParallaxType => parallaxType;

        public float AnimationSpeed { set; get; }
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            meshRenderer.material.mainTextureOffset += new Vector2(AnimationSpeed * Time.deltaTime, meshRenderer.material.mainTextureOffset.y);
        }
    }
}
