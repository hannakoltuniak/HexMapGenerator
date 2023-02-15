using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMouseInteraction : MonoBehaviour
{
    private HexTile _lastHoveredTile;
    
    [SerializeField] private InfoWindow InfoWindow;

    void Update()
    {
        if (GuiStateInfo.IsWindowVisible)
            return;

        Vector2 rayStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.zero);
        if (hit.collider != null)
        {
            HexTile currentlyHovered = hit.transform.GetComponent<HexTile>();
            ShowInfoForInteractableTile(currentlyHovered);

            if (_lastHoveredTile == currentlyHovered)
                return;
            else
            {
                if (_lastHoveredTile)
                    _lastHoveredTile.MouseLeftTile();
                _lastHoveredTile = currentlyHovered;
            }
        }

        _lastHoveredTile?.MouseHoveredOnTile();
    }

    private void ShowInfoForInteractableTile(HexTile currentlyHovered)
    {
        HexTileInfo tileInfo = currentlyHovered.GetTileInfo();
        if (tileInfo.IsInteractable() && Input.GetMouseButtonDown(0))
        {
            InfoWindow.ShowTileInfo(tileInfo);
        }
    }
}
