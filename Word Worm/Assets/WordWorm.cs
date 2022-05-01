using System.Collections;
using UnityEngine;
using TMPro;

public class WordWorm : MonoBehaviour
{
    //Unity Stuff
    public MapLoader map;

    public char[][] wordMap; // map of letters, to find words in

    private TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    /// <summary>
    /// Called when a new map is generated from MapLoader. Only necessary to keep char[][] assignment in this class for presentation. Otherwise, use MapLoader's WordMap.
    /// </summary>
    public void NewMap()
    {
        wordMap = map.WordMap;
    }

    public void NewWord()
    {
        string word = inputField.text.ToUpper();

        if (word.Length == 0) {
            Debug.LogWarning("No word entered into search field!");
            return; 
        }

        if (wordMap == null)
        {
            Debug.LogWarning("wordMap in search is null! Did you generate a map?");
            return;
        }

        Debug.Log("begin search of " + word + "!");

        Solve(word);
    }

    // sets wordFound bool list to the solved answer
    private void Solve(string word)
    {
        // choose a word to search for (looping through map once PER word)      Note: searching for every word at once would not work (at least without an alphabetical search process on the bank itself), as words can have the same starting letters. Or the same 3 letters before a difference, etc.   
        //  loop through every cell
        //  start recursive search when first letter of word is found.
        //      follow recursion with continuing letters, spawning recursion at each up/down/left/right/DIAGONALLY with matching letter for the word.
        //      set wordFound  =  true for the found word if recursion reaches the end of the word
        // break out of recursion as that word has been found (don't keep searching)
        //      keep wordFound = false for the found word if recursion doesn't reach the end of the word.

        char firstLetter = word[0];
        int[][] wordPath = new int[word.Length][];


        //<Unity>
        // reset colors
        map.ResetTiles();
        Debug.Log("new word");
        //<\Unity>

        for (int row = 0; row < wordMap.Length; row++)
        {
            for (int col = 0; col < wordMap[0].Length; col++)
            {
                if (wordMap[row][col] == firstLetter)
                {
                    //<Unity>
                    wordPath[0] = new int[] { row, col };

                    map.GetTile(row, col).MarkSearching();
                    Debug.Log(firstLetter + "(" + row + "," + col + ") " + "Redden");
                    //<\Unity>

                    Search(word, false, row, col, 1, wordPath);
                }
            }
        }
    }

    // recursive search
    private void Search(string word, bool wordFound, int row, int col, int letterIndex, int[][] wordPath)
    {
        if (letterIndex >= word.Length) wordFound = true; // the word is found when every letter has been reached
        if (wordFound)
        {
            //<Unity>
            foreach (int[] coord in wordPath)
            {
                map.GetTile(coord).MarkFound();
            }
            Debug.Log($"Word \"{word}\" Found!");
            //<\Unity>

            return; // if word is found, end the search for it. This is a separate if statement to check if other branches have completed the search
        }

        char targetLetter = word[letterIndex];

        //System.out.println("Search spawned\tat row:" + row + " col:" + col);

        // directions:
        // row - 1  & col       -> up
        // row + 1  & col       -> down

        // row      & col - 1   -> left
        // row      & col + 1   -> right

        // row - 1 & col - 1    -> upLeft
        // row - 1 & col + 1    -> downRight
        // row + 1 & col - 1    -> downLeft
        // row + 1 & col + 1    -> downRight            

        // target domain:
        // 0 <= row + i < wordMap.Length
        // 0 <= col + j < wordMap[0].Length


        for (int i = -1; i <= 1; i++)
        { // loop surrounding rows (inclusive)
            int targetRow = row + i;

            if (targetRow >= 0 && targetRow < wordMap.Length)
            { // if rows in domain
                for (int j = -1; j <= 1; j++)
                { // loop surrounding cols (inclusive)
                    if (!(i == 0 && j == 0))
                    { // if target is moved from current position
                        int targetCol = col + j;

                        if (targetCol >= 0 && targetCol < wordMap[0].Length)
                        { // if cols in domain
                            if (wordMap[targetRow][targetCol] == targetLetter)
                            { // if letter matches target 

                                //<Unity>
                                wordPath[letterIndex] = new int[] { targetRow, targetCol };

                                map.GetTile(row, col).MarkSearching();
                                Debug.Log(targetLetter + "(" + targetRow + "," + targetCol + ") " + "Redden");
                                //<\Unity>

                                Search(word, wordFound, targetRow, targetCol, letterIndex + 1, wordPath); // continue search
                            }
                        }
                    }
                }

            }
        }
        // no word found by this branch if code reached here

        //<Unity>
        // reset colors
        map.GetTile(row, col).MarkResetSearch();
        Debug.Log("@163 resetting coloring (" + row + "," + col + ")");
        //<\Unity>
    }
}