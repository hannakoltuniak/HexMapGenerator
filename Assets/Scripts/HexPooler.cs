using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPooler : InitializableBehaviour
{
    [SerializeField] public GameObject HexPoolablePrefab;

    [SerializeField] private int poolSize = 420;

    private List<GameObject> _hexPool;

    public override void Init()
    {
        _hexPool = new List<GameObject>();
        FillHexPool();
    }

    private void FillHexPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newPoolItem = AddHexToPool();
            newPoolItem.SetActive(false);
        }
    }

    public GameObject GetPooledObjectAndAdjustForHexTileInfo(HexTileInfo tileInfo)
    {
        for (int i = 0; i < _hexPool.Count; i++)
        {
            if (!_hexPool[i].activeInHierarchy)
            {
                _hexPool[i].GetComponent<HexTile>().AdjustForHexTileInfo(tileInfo);
                return _hexPool[i];
            }
        }

        GameObject newHex = AddHexToPool();
        newHex.GetComponent<HexTile>().AdjustForHexTileInfo(tileInfo);
        return newHex;
    }

    private GameObject AddHexToPool()
    {
        GameObject obj = Instantiate(HexPoolablePrefab, transform);
        _hexPool.Add(obj);
        return obj;
    }
}
