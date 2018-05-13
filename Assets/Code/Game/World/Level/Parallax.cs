using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace AshenCode.World.Level
{
    public class Parallax : MonoBehaviour
    {

        public GameObject prefab;
        public int poolSize;
        public float scrollSpeed;
        public float spawnRate;


        public SpawnRange ySpawnRange;
        public SpawnRange xSpawnRange;

        public Vector3 defaultSpawnPosition;
        public bool spawnOnStart;
        public Vector3 startSpawnPosition;
        public Vector2 targetAspectRatio;

        float spawnTimer;
        float targetAspect;
        public static PoolObject[] poolObjects;

        public static Transform closest;

        Coroutine _loopRoutine;


        public void Init()
        {

            Cleanup();

            targetAspect = targetAspectRatio.x / targetAspectRatio.y;
            poolObjects = new PoolObject[poolSize];
            for (int i = 0; i < poolObjects.Length; i++)
            {
                GameObject g = Instantiate(prefab) as GameObject;
                Transform t = g.transform;
                t.SetParent(this.transform);
                t.position = Vector3.one * 1000;
                poolObjects[i] = new PoolObject(t);
            }

            if(spawnOnStart)
            {
                SpawnImmediate();
            }


            _loopRoutine = StartCoroutine(Loop());

        }


        void Cleanup()
        {
            if(poolObjects != null)
            {
                poolObjects.ToList().ForEach(p => p.Dispose());
            }
            if(_loopRoutine != null)
            {
                StopCoroutine(_loopRoutine);
            }
        }

        IEnumerator Loop()
        {
            while(true)
            {
                Scroll();
                spawnTimer += Time.deltaTime;
                if(spawnTimer > spawnRate)
                {
                    Spawn(defaultSpawnPosition, null);
                    spawnTimer = 0;
                }
                yield return new WaitForSeconds(0);
            }
        }

        void Spawn(Vector3 spawnPosition, Action callback=null)
        {
            Transform t = GetPoolObject();
            if(t == null) return;
            Vector3 position = Vector3.zero;
            position.x = spawnPosition.x * Camera.main.aspect / targetAspect ;
            position.y = UnityEngine.Random.Range(ySpawnRange.min, ySpawnRange.max);
            t.position = position;

            

            if(callback != null)
            {
                callback();
            }
        }

        void SpawnImmediate()
        {
            Spawn(defaultSpawnPosition, () => 
            {
                Spawn(startSpawnPosition);
            });
        }

        void Scroll()
        {         

            for (int i = 0; i < poolObjects.Length; i++)
            {
                poolObjects[i].transform.localPosition += -Vector3.right * scrollSpeed * Time.deltaTime;
                CheckDisposables(poolObjects[i]);
            };
        }

        void CheckDisposables(PoolObject poolObj)
        {
            if(poolObj.transform.position.x < (-defaultSpawnPosition.x - poolObj.GetWidth() * Camera.main.aspect / targetAspect))
            {
                poolObj.Dispose();
            }
        }

        Transform GetPoolObject()
        {
            PoolObject poolObj = poolObjects.FirstOrDefault(p => !p.active);

            if(poolObj != null)
            {
                poolObj.Activate();
                return poolObj.transform;
            }
            return null;
        }
        
    }
}