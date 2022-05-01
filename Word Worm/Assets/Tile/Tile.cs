using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public GameObject tile;

    public Transform background;

    public Transform character;

    public Vector2 coordinate;

    public Color color = new Color(1, 0, 0, 0);

    private Image backgroundImage;

    private void Awake()
    {
        backgroundImage = background.GetComponent<Image>();
    }

    public void Initialize(GameObject tile, char new_char, string row, string col)
    {
        tile.name = "("+row+","+col+")";
        character =  tile.transform.Find("Character");
        ChangeCharacter(new_char);
    }
    private void ChangeCharacter(char new_char)
    {
        character.GetComponent<TextMeshProUGUI>().text = new_char.ToString();
    }

    private float HalfStep(float inititalAlpha)
    {
        float step = (1 - inititalAlpha) / 2;
        return inititalAlpha + step;
    }

    /// <summary>
    /// Called when the tile is in progres of being searched.
    /// </summary>
    public void MarkSearching() 
    {
        // when the algorithm locates the next letter, it will redden that tile
        color.a = HalfStep(color.a);
    }

    /// <summary>
    /// Called when the tile is part of a found word.
    /// </summary>
    public void MarkFound() 
    {
        // when the algorithm finds the full word, all red tiles will turn green, showing the word path
        color.g = 1;
        color.r = 0;
    }

    /// <summary>
    /// Called to reset the tile's state, like before all searches or after a failed search branch.
    /// </summary>
    public void MarkResetSearch() 
    {
        // when the algorithm starts on a new word, all colors are reset
        color.r = 1;
        color.g = 0;
        color.a = 0;
    }

    private void Update()
    {
        backgroundImage.color = color;
    }
}
