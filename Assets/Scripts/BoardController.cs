using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BoardController : InitializableBehaviour
{
    public const int BOARD_DIMENSION_X = 100;
    public const int BOARD_DIMENSION_Y = 100;
    public const int BOARD_TILES_COUNT = BOARD_DIMENSION_Y * BOARD_DIMENSION_Y;

    private Vector2 _boardDimensions = new Vector2(BOARD_DIMENSION_X, BOARD_DIMENSION_Y);

    [SerializeField]
    private CameraDrag CameraDrag;

    [SerializeField]
    private HexPooler HexPooler;

    private List<HexTileInfo> _boardData = new List<HexTileInfo>();

    public override void Init()
    {
        SetupInitialVariables();
        GenerateBoardTileDataAccordingToDistributionRequirements();
        CameraDrag.AttachNewCameraDragListener(ShowHexGridInsideEffectiveCameraBounds);
    }

    private void GenerateBoardTileDataAccordingToDistributionRequirements()
    {
        print("Loading level...");

        for (int x = 1; x < _boardDimensions.x + 1; x++)
        {
            for (int y = 1; y < _boardDimensions.y + 1; y++)
            {
                Vector2Int currentVirtualPosition = new Vector2Int(x, y);
                _boardData.Add(new HexTileInfo(currentVirtualPosition));
            }
        }

        TileTypesDistributor.AdjustBoardTilesKindsForDistributionRequirements(_boardData);
        PrintTileTypeCounts();
        print("Level loaded");
    }

    private void PrintTileTypeCounts()
    {
        var blueCount = _boardData.Count(b => b.Type == HexType.Blue);
        var greenCount = _boardData.Count(b => b.Type == HexType.Green);
        var yellowCount = _boardData.Count(b => b.Type == HexType.Yellow);
        var grayCount = _boardData.Count(b => b.Type == HexType.Grey);

        print($"Board distribution check: Blue: {blueCount} | Green: {greenCount} | Yellow: {yellowCount} | Gray: {grayCount}");
    }

    private void SetupInitialVariables()
    {
        Rect tileRect = HexPooler.HexPoolablePrefab.GetComponent<RectTransform>().rect;
        HexTileInfo.TileHeight = tileRect.size.y;
        HexTileInfo.TileWidth = tileRect.size.x;
    }

    private void ShowHexGridInsideEffectiveCameraBounds()
    {
        foreach (HexTileInfo tile in _boardData)
        {
            if (CameraDrag.CurrentCameraEffectiveBounds
                .Overlaps(new Rect(tile.WorldSpacePosition, Vector2.one)))
            {
                if (!tile.WorldSpaceRepresentationOfThisTile)
                    tile.WorldSpaceRepresentationOfThisTile = HexPooler.GetPooledObjectAndAdjustForHexTileInfo(tile);
            }
            else if (tile.WorldSpaceRepresentationOfThisTile)
            {
                tile.WorldSpaceRepresentationOfThisTile.SetActive(false);
                tile.WorldSpaceRepresentationOfThisTile = null;
            }
        }
    }

}
