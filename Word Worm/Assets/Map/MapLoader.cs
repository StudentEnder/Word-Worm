using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    public WordMapScriptableObject map;
    public GameObject tilePrefab;
    public WordWorm wordWorm;

    public char[][] WordMap { get; private set; }

    public void SpawnMap()
    {
        ClearBoard();

        int rows = map.dimensions[0];
        int cols = map.dimensions[1];

        map.GenerateWordMap();
        WordMap = map.wordMap;

        gameObject.GetComponent<GridLayoutGroup>().constraintCount = cols;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                tile.GetComponent<Tile>().Initialize(tile, map.wordMap[i][j], i.ToString(), j.ToString());
            }

        }

        wordWorm.NewMap(); // called to update wordMap reference there.
    }

    private void ClearBoard()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public Tile GetTile(int row, int col)
    {
        return transform.Find("(" + row + "," + col + ")").GetComponent<Tile>();
    }

    public Tile GetTile(int[] coord)
    {
        return GetTile(coord[0], coord[1]);
    }

    public void ResetTiles()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Tile>().MarkResetSearch();
        }
    }
}
