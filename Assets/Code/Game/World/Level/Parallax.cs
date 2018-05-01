using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Parallax : MonoBehaviour
{
	#region PoolObject

	[Serializable]
	class PoolObject
	{
        public Transform transform;
        public bool active;

        Renderer[] _renderers;

        public PoolObject(Transform transform)
		{
            this.transform = transform;
            this._renderers = this.transform.GetComponentsInChildren<SpriteRenderer>();
        }

		public void Activate()
		{
            this.active = true;
            if (this._renderers != null)
            {
                for (int i = 0; i < this._renderers.Length; i++)
				{
                //  this._renderers[i].enabled = true;
				}
            }
        }

		public void Dispose()
		{
            this.active = false;
            this.transform.position = Vector3.one * 1000;
            for (int i = 0; i < this._renderers.Length; i++)
            {
             //   this._renderers[i].enabled = false;
            }
			
        }
    }
	#endregion PoolObject

	#region SpawnRange

	[Serializable]
	public struct SpawnRange
	{
        public float min;
		public float max;
    }

	#endregion

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
    PoolObject[] poolObjects;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
        Init();
    }


	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		
	}

	void Init()
	{
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
    }

	void OnApplicationQuit()
	{
		
	}
	void Update()
	{
        Scroll();
        spawnTimer += Time.deltaTime;
		if(spawnTimer > spawnRate)
		{
            Spawn(defaultSpawnPosition, null);
            spawnTimer = 0;
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
		if(poolObj.transform.position.x < (-defaultSpawnPosition.x * Camera.main.aspect / targetAspect))
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
