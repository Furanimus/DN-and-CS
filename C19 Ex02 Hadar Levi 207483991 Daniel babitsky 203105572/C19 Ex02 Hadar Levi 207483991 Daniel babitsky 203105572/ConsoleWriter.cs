using System;
using System.Collections.Generic;
using System.Text;

namespace C19_Ex02
{
    public class ConsoleWriter
    {
        private StringBuilder output;

        private void printHeader()
        {
            output.AppendLine("Current board status:");
            output.Append("|");
            output.Append("Pins:".PadRight(9));
            output.Append("|");
            output.Append("Result:".PadRight(8));
            output.Append("|");

            output.AppendLine(string.Empty);
        }

        private void printSeperator()
        {
            output.Append("|=========|========|");
        }

        private void printEmptyLine()
        {
            output.Append("|");
            output.Append(" ".PadRight(9));
            output.Append("|");

            output.Append(" ".PadRight(8));
            output.Append("|");
            output.AppendLine(string.Empty);
        }

        public void Write(int totalRows, List<GuessResult> input)
        {
            output = new StringBuilder();

            printHeader();

            printSeperator();
                    
            output.AppendLine(string.Empty);

            output.AppendLine("| # # # # |        |");

            printSeperator();

            output.AppendLine(string.Empty);

            for (int i = 0; i < totalRows; i++)
            {
                if (i >= input.Count) 
                {
                    printEmptyLine();
                }
                else
                {
                    var line = input[i];
                    
                    output.Append("| ");

                    for (int j = 0; j < line.m_Guess.Length; j++)
                    {
                        output.Append(line.m_Guess[j] + " ");
                    }

                    output.Append("|");

                    for (int j = 0; j < line.m_ExactMatch; j++)
                    {
                        output.Append("V ");                    
                    }

                    for (int j = 0; j < line.m_NotExactMatch; j++)
                    {
                        output.Append("X ");
                    }

                    int spaces = 8 - ((line.m_ExactMatch + line.m_NotExactMatch) * 2);

                    for (int j = 0; j < spaces; j++)
                    {
                        output.Append(" ");    
                    }
                                        
                    output.Append("|");

                    output.AppendLine(string.Empty);                    
                }

                printSeperator();

                output.AppendLine(string.Empty);
            }

            Console.WriteLine(output.ToString());
        }
    }
}
