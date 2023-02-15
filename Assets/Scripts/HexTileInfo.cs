using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HexTileInfo
{
    public static float TileHeight;
    public static float TileWidth;

    public Vector2Int VirtualMapPosition;
    public Vector2 WorldSpacePosition;
    public HexType Type;

    public GameObject WorldSpaceRepresentationOfThisTile;

    public HexTileInfo(Vector2Int virtualMapPosition)
    {
        VirtualMapPosition = virtualMapPosition;
        WorldSpacePosition = GetTilePositionBasedOnCoordinates(virtualMapPosition);
    }

    public bool IsInteractable() => Type == HexType.Yellow || Type == HexType.Green;

    private Vector3 GetTilePositionBasedOnCoordinates(Vector2Int virtualPos)
    {
        bool isOffsetted = (virtualPos.y % 2) == 0;
        return new Vector3(virtualPos.x * TileWidth + (isOffsetted ? TileWidth / 2f : 0f),
            virtualPos.y * TileHeight * 0.75f);
    }
}
