using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowTeeGames
{
	[AddComponentMenu("LowTee/Pool")]
	public class LTPool : LTSingleton<LTPool>
	{
        [Header("Prefabs that will be part of the pool, you don't need to specify the prefabs as they will automatically be added to the pool")]
        public List<GameObject> objectsToPool;
        public int numberToCreateAtStart;
        [Header("Parent gameobject of the pooled items, if null, they will stay at the scene root")]
        public Transform poolParent;

        private Dictionary<GameObject, List<GameObject>> pool = new Dictionary<GameObject, List<GameObject>>();

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < objectsToPool.Count; i++)
            {
                AddToPool(objectsToPool[i]);
            }
        }

        private void AddToPool(GameObject item)
        {
            pool.Add(item, new List<GameObject>());
            for (int i = 0; i < numberToCreateAtStart; i++)
            {
                GameObject newItem = Instantiate(item);
                newItem.SetActive(false);
                pool[item].Add(newItem);
                newItem.transform.SetParent(poolParent);
            }
        }

        private GameObject GetNewItem(List<GameObject> itemList)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (!itemList[i].activeSelf)
                {
                    return itemList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a pooled item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject GetItemFromPool(GameObject item, Vector3 position, Quaternion rotation)
        {
            if (!pool.ContainsKey(item))
            {
                AddToPool(item);
            }

            GameObject newItem = GetNewItem(pool[item]);

            if (!newItem)
            {
                newItem = Instantiate(item);
                pool[item].Add(newItem);
                if (poolParent != null)
                {
                    newItem.transform.SetParent(poolParent);
                }
            }
            newItem.transform.position = position;
            newItem.transform.rotation = rotation;
            newItem.SetActive(true);

            return newItem;
        }
    }
}