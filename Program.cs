using System.Text.RegularExpressions;
public class Solution {

    static void Main(string[] args)
        {
            Solution test = new Solution();
            Console.WriteLine(test.NumberToWords(1000000));
        }

    public string NumberToWords(int num) {
        string integerWord = "";

        Dictionary<int, string> oneDigit = new Dictionary<int, string>()
        {
            {1, "One"},
            {2, "Two"},
            {3, "Three"},
            {4, "Four"},
            {5, "Five"},
            {6, "Six"},
            {7, "Seven"},
            {8, "Eight"},
            {9, "Nine"},
        };

        Dictionary<int, string> teens = new Dictionary<int, string>()
        {
            {10, "Ten"},
            {11, "Eleven"}, 
            {12, "Twelve"},
            {13, "Thirteen"},
            {14, "Fourteen"},
            {15, "Fifteen"},
            {16, "Sixteen"},
            {17, "Seventeen"},
            {18, "Eighteen"},
            {19, "Nineteen"}
        };

        Dictionary<int, string> twoDigit = new Dictionary<int, string>()
        {
            {1, "Ten"},
            {2, "Twenty"},
            {3, "Thirty"},
            {4, "Forty"},
            {5, "Fifty"},
            {6, "Sixty"},
            {7, "Seventy"},
            {8, "Eighty"},
            {9, "Ninety"},
        };

        string[] powerOfTenWords = {"", "Thousand", "Million", "Billion"};   

        // Check if 0
        if (num == 0)
        {
            return "Zero";
        }
       
        int runs = 0;
        string tempWord;
        string hundreds;
        while (num > 0 && runs < 10)
        {
            tempWord = "";
            hundreds = "";
            // Check for teens
            tempWord = DigitText(ref num, teens, 100, 10, 19);

            // If not teens, check last digit
            if (tempWord.Length == 0)
            {
                tempWord = DigitText(ref num, oneDigit, 10, 1, 9);
                // Find two digit word
                if (tempWord.Length > 0)
                {
                    tempWord = DigitText(ref num, twoDigit, 10, 2, 9) + " " + tempWord;
                }
                else 
                {
                    tempWord = DigitText(ref num, twoDigit, 10, 2, 9);
                }
                
            }

            // Check for hundreds
            hundreds = DigitText(ref num, oneDigit, 10, 1, 9);
            if (hundreds.Length > 0)
            {
                tempWord = hundreds + " Hundred " + tempWord;
            }

            if (tempWord.Length > 0)
            {
                integerWord = tempWord + " " + powerOfTenWords[runs]+ " " + integerWord;
            }
            runs += 1;
        }

        if (runs == 10){return "Max Runs";}

        integerWord = integerWord.Trim();
        RegexOptions options = RegexOptions.None;
        Regex regex = new Regex("[ ]{2,}", options);     
        integerWord = regex.Replace(integerWord, " ");
        return integerWord;
    }

    public string DigitText(ref int number, Dictionary<int, string> dict, int denominator, int lowerLimit, int upperLimit){
        string digitWord = "";
        int modulo = number % denominator;
        if (modulo >= lowerLimit && modulo <= upperLimit)
        {
            digitWord += dict[modulo];
            number -= modulo;
            number = number/denominator;
        }
        else if (modulo == 0 && denominator != 100)
        {
            number = number/denominator;
        }
            
        return digitWord;
    }
}
