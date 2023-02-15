using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts
{
    public static class TileTypesDistributor
    {
        public static void AdjustBoardTilesKindsForDistributionRequirements(IList<HexTileInfo> boardData)
        {
            List<int> randomDistributionIndexSource = Enumerable.Range(0, BoardController.BOARD_TILES_COUNT)
                .OrderBy(item => UnityEngine.Random.Range(-1, 1f))
                .ToList();

            Dictionary<HexType, float> distributionRequirements = new Dictionary<HexType, float>()
            {
                { HexType.Blue, 60f},
                { HexType.Green, 10f},
                { HexType.Yellow, 5f},
                { HexType.Grey, 25f}
            };

            float count = distributionRequirements.Sum(el => el.Value);

            if (count != 100f)
            {
                UnityEngine.Debug.LogError("Distributions need to add up to 100!");
                return;
            }

            int lastIndexDistributed = 0;
            foreach (var distribution in distributionRequirements)
            {
                int i = 0;
                for (i = lastIndexDistributed; i < lastIndexDistributed + distribution.Value / 100f * BoardController.BOARD_TILES_COUNT; i++)
                {
                    boardData[randomDistributionIndexSource[i]].Type = distribution.Key;
                }
                lastIndexDistributed = i;
            }

        }

    }
}
