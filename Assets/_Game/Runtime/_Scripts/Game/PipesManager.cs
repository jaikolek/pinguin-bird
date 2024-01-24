using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguinBird.Game
{
    public class PipesManager : SceneSingleton<PipesManager>
    {
        [SerializeField] private GameObject pipePrefab;

        [SerializeField] private float spawnRate = 1f;  //  2 until 1
        public float SpawnRate
        {
            set
            {
                spawnRate = value;
            }

            get => spawnRate;
        }

        [SerializeField] private float minHeight = -1f;
        public float MinHeight
        {
            set
            {
                minHeight = value;
            }

            get => minHeight;
        }

        [SerializeField] private float maxHeight = 1f;
        public float MaxHealth
        {
            set
            {
                maxHeight = value;
            }

            get => maxHeight;
        }

        private float speed;
        public float Speed
        {
            set
            {
                speed = value;
                ChangePipesSpeed(value);
                CancelInvoke(nameof(Spawn));
                InvokeRepeating(nameof(Spawn), SpawnRate, SpawnRate);
            }

            get => speed;
        }

        private void OnEnable()
        {
            InvokeRepeating(nameof(Spawn), SpawnRate, SpawnRate);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Spawn));
        }

        private void Spawn()
        {
            GameObject pipe = Instantiate(pipePrefab, this.transform.position, Quaternion.identity);
            pipe.GetComponent<Pipe>().MoveSpeed = Speed;
            pipe.transform.SetParent(this.transform.parent);
            pipe.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        }

        private void ChangePipesSpeed(float speed)
        {
            Pipe[] pipes = FindObjectsOfType<Pipe>();

            for (int i = 0; i < pipes.Length; i++)
            {
                pipes[i].MoveSpeed = speed;
            }
        }
    }
}
