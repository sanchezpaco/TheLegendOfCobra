using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Managers
{
	public class PoolManager : MonoBehaviour
	{
	
		#region Public Properties
		public bool createPoolAtStartup;
		public List<GameObject> availableObjects;
		public float itemsPerObjectDefault = 10;
		public Transform parent = null;
		public Vector3 position = Vector3.zero;
		#endregion

		#region Private Properties
		private List<GameObject> objectsPool;
		#endregion

		#region MonoBehaviour Components
		void Awake ()
		{
			this.objectsPool = new List<GameObject> ();
		}

		void Start() {
			if (this.createPoolAtStartup) {
				this.CreatePool ();
			}
		}
		#endregion

		#region Public Properties
		public GameObject Get ()
		{

			GameObject toReturn = null;

			if (this.objectsPool.Count > 0) {
				toReturn = this.objectsPool [0];
				this.objectsPool.Remove (toReturn);
			}

			return toReturn;
		}

		public int GetPoolLength() {
			return this.objectsPool.Count;
		}

		public void Save (GameObject toSave)
		{
			toSave.transform.parent = this.parent;
			toSave.transform.localPosition = this.position;
			toSave.gameObject.SetActive (false);
			this.objectsPool.Add (toSave);
		}

		public void FlushPool ()
		{
			for (int i = 0, objectPoolLength = this.objectsPool.Count; i < objectPoolLength; i++) {
				this.objectsPool.RemoveAt (0);
			}
		}

		public void CreatePool ()
		{

			for (int i = 0, availableObjectsLength = this.availableObjects.Count; i < availableObjectsLength; i++) {
				for (int j = 0; j < this.itemsPerObjectDefault; j++) {
					GameObject objIns = GameObject.Instantiate<GameObject> (this.availableObjects [i]);
					objIns.transform.parent = this.parent;
					objIns.transform.localPosition = position;
					this.Save (objIns);
				}
			}

			this.objectsPool.Shuffle ();
		}

		public void CreatePool (Dictionary<string, int> itemsQuantity)
		{
		
			GameObject availableObject = null;

			for (int i = 0, availableObjectsLength = this.availableObjects.Count; i < availableObjectsLength; i++) {
				availableObject = this.availableObjects [i];
				for (int j = 0, itemLength = itemsQuantity[availableObject.name]; j < itemLength; j++) {
					GameObject objIns = GameObject.Instantiate<GameObject> (availableObject);
					objIns.transform.parent = this.parent;
					objIns.transform.localPosition = position;
					this.Save (objIns);
				}
			}

			this.objectsPool.Shuffle ();
		}
		#endregion

	}
}
