using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Word Map", menuName = "Word Map")]
public class WordMapScriptableObject : ScriptableObject
{
    [SerializeField]
    public Transform grid;

    private System.Random rand = new System.Random();

    public int[] dimensions = new int[2] {8,8};
    public char[][] wordMap;

    private char RandomCharacter(System.Random rand)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int num = rand.Next(0, chars.Length);
        return chars[num];
    }

    public void GenerateWordMap()
    {
        int rows = dimensions[0];
        int cols = dimensions[1];

        char[][] map = new char[rows][];

        for (int i = 0; i < rows; i++)
        {
            char[] row = new char[cols];

            for (int j = 0; j < cols; j++)
            {
                row[j] = RandomCharacter(rand);
            }
            map[i] = row;
        }

        wordMap = map;
    }
}
