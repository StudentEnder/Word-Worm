using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Word Map", menuName = "Word Map")]
public class WordMapScriptableObject : ScriptableObject
{
    public int[] size = new int[2];
    public char[][] map =
    {
        new char[] {'A', 'B', 'C'},
        new char[] {'D', 'E', 'F'},
        new char[] {'G', 'H', 'I'}
    };
}
