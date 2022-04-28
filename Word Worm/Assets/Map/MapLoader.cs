using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    public GameObject grid;
    public WordMapScriptableObject map;
    public GameObject tilePrefab;

    public void SpawnMap()
    {
        ClearBoard();

        grid.GetComponent<GridLayoutGroup>().constraintCount = map.size[0];

        for (int i = 0; i < map.size[0]; i++)
        {
            for (int j = 0; j < map.size[1]; j++)
            {
                GameObject tile = Instantiate(tilePrefab, grid.transform);
                tile.GetComponent<Tile>().Initialize(tile, map.map[i][j], j.ToString(), i.ToString());
            }

        }
    }

    private void ClearBoard()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
