using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private GameObject HexSelector;

    private HexTileInfo _currentPresentedHex;
    public HexTileInfo GetTileInfo() => _currentPresentedHex;

    public void MouseHoveredOnTile()
    {
        HexSelector.SetActive(true);
    }

    public void MouseLeftTile()
    {
        HexSelector.SetActive(false);
    }

    internal void AdjustForHexTileInfo(HexTileInfo tileInfo)
    {
        _currentPresentedHex = tileInfo;
        transform.position = tileInfo.WorldSpacePosition;
        GetComponent<SpriteRenderer>().color = HexTypeHelper.GetColorFromHexType(tileInfo.Type);
        gameObject.SetActive(true);
    }
}
