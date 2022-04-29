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

        int rows = map.dimensions[0];
        int cols = map.dimensions[1];

        map.GenerateWordMap(map.dimensions);

        grid.GetComponent<GridLayoutGroup>().constraintCount = cols;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
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
