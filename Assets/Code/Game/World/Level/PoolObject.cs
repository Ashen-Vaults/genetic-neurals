namespace AshenCode.World.Level
{
    #region PoolObject

	[Serializable]
	public class PoolObject
	{
        public Transform transform;
        public bool active;

        Renderer[] _renderers;

        public PoolObject(Transform transform)
		{
            this.transform = transform;
            this._renderers = this.transform.GetComponentsInChildren<SpriteRenderer>();
        }

        public float GetWidth()
        {
            float width = 0f;
            for (int i = 0; i < this._renderers.Length; i++)
            {
                width += this._renderers[i].bounds.size.x;
            }
            return width;
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
}