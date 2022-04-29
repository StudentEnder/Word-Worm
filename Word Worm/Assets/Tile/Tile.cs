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

    private float HalfStep(float init)
    {
        float step = (1 - init) / 2;
        return init + step;
    }

    public IEnumerator Redden() // when the algorithm locates the next letter, it will redden that tile
    {
        color.a = HalfStep(color.a);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Found() // when the algorithm finds the full word, all red tiles will turn green, showing the word path
    {
        color.g = color.r;
        color.r = 0;
        yield return new WaitForSeconds(1f);
    }

    public void NewWord() // when the algorithm starts on a new word, all colors are reset
    {
        color.r = 1;
        color.g = 0;
        color.a = 0;
    }

    private void Update()
    {
        background.GetComponent<Image>().color = color;
    }
}
