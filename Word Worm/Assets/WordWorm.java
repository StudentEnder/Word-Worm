import java.util.ArrayList;
import java.util.Scanner;

public class WordWorm {

    public static class TestCase {
        public char[][] wordMap; // map of letters, to find words in
        public String[] wordBank; // bank of words to search for
        public boolean[] wordFound; // whether or not word at the same index in wordBank is found. boolean list must be in the same order as wordBank list

        public TestCase(char[][] wordMap, String[] wordBank) {
            this.wordMap = wordMap;
            this.wordBank = wordBank;

            // initialize wordFound as a false-filled array.
            wordFound = new boolean[wordBank.length];
            for (int i = 0; i < wordFound.length; i++) {
                wordFound[i] = false;
            }
        }

        // sets wordFound boolean list to the solved answer
        public void solve() {
            // choose a word to search for (looping through map once PER word)      Note: searching for every word at once would not work (at least without an alphabetical search process on the bank itself), as words can have the same starting letters. Or the same 3 letters before a difference, etc.   
            //  loop through every cell
            //  start recursive search when first letter of word is found.
            //      follow recursion with continuing letters, spawning recursion at each up/down/left/right/DIAGONALLY with matching letter for the word.
            //      set wordFound  =  true for the found word if recursion reaches the end of the word
                        // break out of recursion as that word has been found (don't keep searching)
            //      keep wordFound = false for the found word if recursion doesn't reach the end of the word.
            for (int wordBankIndex = 0; wordBankIndex < wordBank.length; wordBankIndex++) {
                String word = wordBank[wordBankIndex];
                char firstLetter = word.charAt(0);
                for (int row = 0; row < wordMap.length; row++) {
                    for (int col = 0; col < wordMap[0].length; col++) {
                        if (wordMap[row][col] == firstLetter) {
                            //System.out.println("Started search for \"" + word + "\" at row:" + row + " col:" + col);
                            search(word, wordBankIndex, row, col, 1);
                        }
                    }
                }
            }
        }

        // recursive search
        private void search(String word, int wordBankIndex, int row, int col, int letterIndex) {
            if (letterIndex >= word.length()) wordFound[wordBankIndex] = true; // the word is found when every letter has been reached
            if (wordFound[wordBankIndex]) return; // if word is found, end the search for it. This is a separate if statement to check if other branches have completed the search

            char targetLetter = word.charAt(letterIndex);

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
            // 0 <= row + i < wordMap.length
            // 0 <= col + j < wordMap[0].length


            for (int i = -1; i <= 1; i++) { // loop surrounding rows (inclusive)
                int targetRow = row + i;

                if (targetRow >= 0 && targetRow < wordMap.length) { // if rows in domain
                    for (int j = -1; j <= 1; j++) { // loop surrounding cols (inclusive)
                        if (!(i == 0 && j == 0)) { // if target is moved from current position
                            int targetCol = col + j;
    
                            if (targetCol >= 0 && targetCol < wordMap[0].length) { // if cols in domain
                                if (wordMap[targetRow][targetCol] == targetLetter) { // if letter matches target 
                                    search(word, wordBankIndex, targetRow, targetCol, letterIndex + 1); // continue search
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
        public void printResult() {
            for (int i = 0; i < wordBank.length; i++) {
                if (wordFound[i]) System.out.println(wordBank[i]);
            }
        }
    }

    public static void main(String[] args) {
        String[] lines = inLinesToString(); // take standard input

        TestCase[] testCases = getTestCases(lines); // parse the input

        for (TestCase testCase : testCases) { 
            testCase.solve(); // solve the test case
            testCase.printResult(); // display the results
        }
    }

    // get each test case's word map and word bank from input.
    public static TestCase[] getTestCases(String[] inputLines) {
        TestCase[] cases = new TestCase[Integer.parseInt(inputLines[0])];

        int lineNumber = 1;
        for (int caseNumber = 0; caseNumber < cases.length; caseNumber++) {
            // get word map
            String[] rowCol = inputLines[lineNumber].split(" ");
            int rows = Integer.parseInt(rowCol[0]);
            int cols = Integer.parseInt(rowCol[1]);

            lineNumber++; // increment line number each time a line is read to move forward in the input independent of cases.

            char[][] wordMap = new char[rows][cols];
            for (int row = 0; row < wordMap.length; row++, lineNumber++) {
                wordMap[row] = getCharsLine(inputLines[lineNumber]); // each row is a char array of the map's letters.
            }

            // get word bank
            int amountOfWords = Integer.parseInt(inputLines[lineNumber]);
            String[] wordBank = new String[amountOfWords];
            
            lineNumber++;

            for (int i = 0; i < wordBank.length; i++, lineNumber++) {
                wordBank[i] = inputLines[lineNumber];
            }

            // create and store the TestCase
            cases[caseNumber] = new TestCase(wordMap, wordBank);
        }

        return cases;
    }

    // converts string to char array after removing string spaces 
    public static char[] getCharsLine(String line) {
        String noSpaces = "";
        for (int i = 0; i < line.length(); i++) {
            char letter = line.charAt(i);
            if (letter != ' ') noSpaces += letter;
        }
        return noSpaces.toCharArray();
    }

    // standard input: converts System.in input (file) to a String array, where each element is one line in the file.
    public static String[] inLinesToString() {
        Scanner input = new Scanner(System.in);
        ArrayList<String> lines = new ArrayList<String>();
        while(input.hasNextLine()) {
            lines.add(input.nextLine());
        }
        input.close();
        String[] linesArray = new String[lines.size()];
        linesArray = lines.toArray(linesArray);
        return linesArray;
    }
}