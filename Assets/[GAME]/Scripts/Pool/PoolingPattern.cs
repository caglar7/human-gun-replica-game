using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  pooling
/// </summary>

namespace GAME
{
    public class PoolingPattern : MonoBehaviour
    {
        #region Properties, Constructor
        private GameObject prefab;
        private Stack<GameObject> objPool = new Stack<GameObject>();

        public PoolingPattern(GameObject prefab)
        {
            this.prefab = prefab;
        }
        #endregion

        #region Methods
        public void FillPool(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject obje = Object.Instantiate(prefab);
                obje.hideFlags = HideFlags.HideInHierarchy;
                AddObjToPool(obje);
            }
        }

        public GameObject PullObjFromPool(HideFlags flags = HideFlags.None)
        {
            if (objPool.Count > 0)
            {
                GameObject obje = objPool.Pop();
                obje.gameObject.SetActive(true);
                obje.hideFlags = flags;

                return obje;
            }

            GameObject objeIns = Object.Instantiate(prefab);
            objeIns.hideFlags = flags;
            return objeIns;
        }

        public void AddObjToPool(GameObject obje)
        {
            obje.transform.SetParent(null);
            obje.gameObject.SetActive(false);
            obje.hideFlags = HideFlags.HideInHierarchy;
            objPool.Push(obje);
        } 

        #endregion
    }
}
