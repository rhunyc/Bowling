using BowlingConsoleApp.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingConsoleApp.Helpers
{
    public static class OutputHelper
    {
        // This will print the scoreboard, using additional helper functions inside this class
        public static void PrintScoreBoard(List<Frame> frames, Frame frame = null)
        {
            if (frame is null)
            {
                Console.WriteLine("GAME END RESULTS - PRESS ANY KEY TO EXIT");
            } else
            {
                Console.WriteLine("*FRAME " + frame.FrameNumber);
            }
            PrintTopOrBottom(frames);
            PrintBalls(frames);
            PrintScores(frames);
            PrintTopOrBottom(frames);
        }

        // Prints the top or bottom border
        private static void PrintTopOrBottom(List<Frame> frames)
        {
            string line = "";
            foreach (var frame in frames)
            {
                line += "*******";
            }
            Console.WriteLine(line + "*");
        }

        // Prints what scores each ball is
        private static void PrintBalls(List<Frame> frames)
        {
            string line = "*";

            foreach (var frame in frames)
            {
                string firstPins = GetPrintablePinsHit(frame.FirstBall);
                string secondPins = frame.SecondBall == null ? "-" : GetPrintablePinsHit(frame.SecondBall);

                if (frame.FrameNumber != 10)
                {
                    line += "   " + firstPins + "|" + secondPins+ "*";

                } 
                else
                {                   
                    string thirdPins = "|" + (frame.ThirdBall == null ? "-" : GetPrintablePinsHit(frame.ThirdBall));
                    line += " " + firstPins + "|" + secondPins + thirdPins + "*";
                }
            }
            Console.WriteLine(line);
        }

        // Prints the scores of each frame individually
        private static void PrintScores(List<Frame> frames)
        {
            string line = "*";

            foreach (var frame in frames)
            {
                if (frame.Score == 0)
                {
                    line += "      ";
                } else if (frame.Score < 10)
                {
                    line += "     " + Convert.ToString(frame.Score);
                } else if (frame.Score >= 10 && frame.Score < 100)
                {
                    line += "    " + Convert.ToString(frame.Score);
                } else
                {
                    line += "  " + Convert.ToString(frame.Score);
                }
                line += "*";
            }

            Console.WriteLine(line);
        }

        // Prints the final score when a round is over
        public static void PrintFinalScore(List<Frame> frames)
        {
            int finalScore = 0;
            foreach (var frame in frames)
            {
                finalScore += frame.Score;
            }

            Console.WriteLine("YOUR FINAL SCORE IS: " + Convert.ToInt32(finalScore));
        }

        // Gets a value for printing the ball's number of pins hit
        public static string GetPrintablePinsHit(Ball ball)
        {
            if (ball.IsSpare)
            {
                return "/";
            }
            else if (ball.IsStrike)
            {
                return "X";
            }
            else
            {
                return Convert.ToString(ball.PinsHit);
            }

        }

    }
}
