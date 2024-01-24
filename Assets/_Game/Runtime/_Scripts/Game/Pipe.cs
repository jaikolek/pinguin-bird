using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class Pipe : MonoBehaviour
    {
        public float MoveSpeed { set; get; }
        private float leftEdge;

        private void Start()
        {
            leftEdge = MainCamController.i.ScreenToWorldPos(Vector3.zero).x - 5f;
        }

        private void Update()
        {
            this.transform.position += Vector3.left * MoveSpeed * Time.deltaTime;

            if (transform.position.x < leftEdge)
            {
                Destroy(gameObject);
            }
        }
    }
}
