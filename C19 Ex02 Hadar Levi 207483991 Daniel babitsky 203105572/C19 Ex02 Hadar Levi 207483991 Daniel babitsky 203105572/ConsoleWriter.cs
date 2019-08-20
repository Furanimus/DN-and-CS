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
            this.output.AppendLine("Current board status:");
            this.output.Append("|");
            this.output.Append("Pins:".PadRight(9));
            this.output.Append("|");
            this.output.Append("Result:".PadRight(8));
            this.output.Append("|");

            this.output.AppendLine(string.Empty);
        }

        private void printSeperator()
        {
            this.output.Append("|=========|========|");
        }

        private void printEmptyLine()
        {
            this.output.Append("|");
            this.output.Append(" ".PadRight(9));
            this.output.Append("|");

            this.output.Append(" ".PadRight(8));
            this.output.Append("|");
            this.output.AppendLine(string.Empty);
        }

        public void Write(int totalRows, List<GuessResult> input)
        {
            this.output = new StringBuilder();
            this.printHeader();
            this.printSeperator();
            this.output.AppendLine(string.Empty);
            this.output.AppendLine("| # # # # |        |");
            this.printSeperator();
            this.output.AppendLine(string.Empty);

            for (int i = 0; i < totalRows; i++)
            {
                if (i >= input.Count) 
                {
                    this.printEmptyLine();
                }
                else
                {
                    var line = input[i];
                    this.output.Append("| ");

                    for (int j = 0; j < line.m_Guess.Length; j++)
                    {
                        this.output.Append(line.m_Guess[j] + " ");
                    }

                    this.output.Append("|");

                    for (int j = 0; j < line.m_ExactMatch; j++)
                    {
                        this.output.Append("V ");                    
                    }

                    for (int j = 0; j < line.m_NotExactMatch; j++)
                    {
                        this.output.Append("X ");
                    }

                    int spaces = 8 - ((line.m_ExactMatch + line.m_NotExactMatch) * 2);

                    for (int j = 0; j < spaces; j++)
                    {
                        this.output.Append(" ");    
                    }

                    this.output.Append("|");
                    this.output.AppendLine(string.Empty);                    
                }

                this.printSeperator();
                this.output.AppendLine(string.Empty);
            }

            Console.WriteLine(this.output.ToString());
        }
    }
}
