using UnityEngine;

public static class WordWorm
{

    public class TestCase
    {
        public char[][] wordMap; // map of letters, to find words in
        public string[] wordBank; // bank of words to Search for
        public bool[] wordFound; // whether or not word at the same index in wordBank is found. bool list must be in the same order as wordBank list

        public TestCase(char[][] wordMap, string[] wordBank)
        {
            this.wordMap = wordMap;
            this.wordBank = wordBank;

            // initialize wordFound as a false-filled array.
            wordFound = new bool[wordBank.Length];
            for (int i = 0; i < wordFound.Length; i++)
            {
                wordFound[i] = false;
            }
        }

        // sets wordFound bool list to the solved answer
        public void Solve()
        {
            // choose a word to search for (looping through map once PER word)      Note: searching for every word at once would not work (at least without an alphabetical search process on the bank itself), as words can have the same starting letters. Or the same 3 letters before a difference, etc.   
            //  loop through every cell
            //  start recursive search when first letter of word is found.
            //      follow recursion with continuing letters, spawning recursion at each up/down/left/right/DIAGONALLY with matching letter for the word.
            //      set wordFound  =  true for the found word if recursion reaches the end of the word
            // break out of recursion as that word has been found (don't keep searching)
            //      keep wordFound = false for the found word if recursion doesn't reach the end of the word.
            for (int wordBankIndex = 0; wordBankIndex < wordBank.Length; wordBankIndex++)
            {
                string word = wordBank[wordBankIndex];
                char firstLetter = word[0];
                for (int row = 0; row < wordMap.Length; row++)
                {
                    for (int col = 0; col < wordMap[0].Length; col++)
                    {
                        if (wordMap[row][col] == firstLetter)
                        {
                            //System.out.println("Started search for \"" + word + "\" at row:" + row + " col:" + col);
                            Search(word, wordBankIndex, row, col, 1);
                        }
                    }
                }
            }
        }

        // recursive search
        private void Search(string word, int wordBankIndex, int row, int col, int letterIndex)
        {
            if (letterIndex >= word.Length) wordFound[wordBankIndex] = true; // the word is found when every letter has been reached
            if (wordFound[wordBankIndex]) return; // if word is found, end the search for it. This is a separate if statement to check if other branches have completed the search

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
                                    Search(word, wordBankIndex, targetRow, targetCol, letterIndex + 1); // continue search
                                }
                            }
                        }
                    }

                }
            }
            //System.out.println("Search died\tat row:" + row + " col:" + col);
            // no word found by this branch if code reached here
        }

        // must first run solve() to get and store the result.
        public void PrintResult()
        {
            for (int i = 0; i < wordBank.Length; i++)
            {
                if (wordFound[i]) Debug.Log(wordBank[i]);
            }
        }
    }

    public static void Main(string[] args)
    {
        string[] lines = inLinesTostring(); // take standard input

        TestCase[] testCases = GetTestCases(lines); // parse the input

        foreach (TestCase testCase in testCases)
        {
            testCase.Solve(); // solve the test case
            testCase.PrintResult(); // display the results
        }
    }

    // get each test case's word map and word bank from input.
    public static TestCase[] GetTestCases(string[] inputLines)
    {
        TestCase[] cases = new TestCase[int.Parse(inputLines[0])];

        int lineNumber = 1;
        for (int caseNumber = 0; caseNumber < cases.Length; caseNumber++)
        {
            // get word map
            string[] rowCol = inputLines[lineNumber].Split(' ');
            int rows = int.Parse(rowCol[0]);
            int cols = int.Parse(rowCol[1]);

            lineNumber++; // increment line number each time a line is read to move forward in the input independent of cases.

            char[][] wordMap = new char[rows][];
            for (int row = 0; row < wordMap.Length; row++, lineNumber++)
            {
                wordMap[row] = GetCharsLine(inputLines[lineNumber]); // each row is a char array of the map's letters.
            }

            // get word bank
            int amountOfWords = int.Parse(inputLines[lineNumber]);
            string[] wordBank = new string[amountOfWords];

            lineNumber++;

            for (int i = 0; i < wordBank.Length; i++, lineNumber++)
            {
                wordBank[i] = inputLines[lineNumber];
            }

            // create and store the TestCase
            cases[caseNumber] = new TestCase(wordMap, wordBank);
        }

        return cases;
    }

    // converts string to char array after removing string spaces 
    public static char[] GetCharsLine(string line)
    {
        string noSpaces = "";
        for (int i = 0; i < line.Length; i++)
        {
            char letter = line[i];
            if (letter != ' ') noSpaces += letter;
        }
        return noSpaces.ToCharArray();
    }

    // standard input: converts System.in input (file) to a string array, where each element is one line in the file.
    public static string[] inLinesTostring()
    {
        Scanner input = new Scanner(System.in);
        ArrayList<string> lines = new ArrayList<string>();
        while (input.hasNextLine())
        {
            lines.add(input.nextLine());
        }
        input.close();
        string[] linesArray = new string[lines.size()];
        linesArray = lines.toArray(linesArray);
        return linesArray;
    }
}