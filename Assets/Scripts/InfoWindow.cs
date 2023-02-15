using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text txtColor;

    [SerializeField] private TMP_Text txtCoords;

    [SerializeField] private Image imgColor;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    public void ShowTileInfo(HexTileInfo tileInfo)
    {
        GuiStateInfo.IsWindowVisible = true;

        txtColor.text = $"{tileInfo.Type.ToString().Split('.').Last()}";
        txtCoords.text = $"X: {tileInfo.VirtualMapPosition.x.ToString()} Y {tileInfo.VirtualMapPosition.y.ToString()}";
        imgColor.color = HexTypeHelper.GetColorFromHexType(tileInfo.Type);

        LeanTween.scale(gameObject, Vector3.one, 0.5f)
            .setEaseOutSine();
    }

    public void Close()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.3f)
            .setEaseOutSine()
            .setOnComplete(() => GuiStateInfo.IsWindowVisible = false);
    }
}
