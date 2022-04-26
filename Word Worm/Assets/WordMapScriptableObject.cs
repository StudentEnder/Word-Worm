using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Word Map", menuName = "Word Map")]
public class WordMapScriptableObject : ScriptableObject
{
    [TextArea] public char[,] map = new char[3, 3]
    {
        {'a', 'b', 'c'},
        {'d', 'e', 'f'},
        {'g', 'h', 'i'}
    };
}
