using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TileModifier : MonoBehaviour
{
    public Vector2 coordinate;

    public GameObject tile;

    public Color color = new Color(1, 0, 0, 0);

    public TextMeshProUGUI character;

    private float HalfStep(float init)
    {
        float step = (1 - init) / 2;
        return init + step;
    }

    public void Redden() // when the algorithm locates the next letter, it will redden that tile
    {
        color.a = HalfStep(color.a);
    }

    public void Found() // when the algorithm finds the full word, all red tiles will turn green, showing the word path
    {
        color.g = color.r;
        color.r = 0;
    }

    public void NewWord() // when the algorithm starts on a new word, all colors are reset
    {
        color.r = 1;
        color.g = 0;
        color.a = 0;
    }

    private void Update()
    {
        tile.GetComponent<Image>().color = color;
    }
}
