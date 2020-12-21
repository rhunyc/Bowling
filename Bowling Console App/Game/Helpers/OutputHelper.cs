using Bowling_Console_App.Game.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling_Console_App.Game.Helpers
{
    public static class OutputHelper
    {
        public static void PromptUserForPinsHit(int ballsRolled)
        {
            Console.WriteLine("Please enter # of pins knocked down for ball " + (ballsRolled + 1) + ":"); // Balls rolled references how many balls were thrown before this, so if we're at 0 the ball we've just thrown is 1
        }

        public static void PromptUserForValidInput(int previousShot)
        {
            string maxAcceptablePoints = Convert.ToString(10 - previousShot);
            Console.WriteLine("Please make sure you enter a valid integer (0-" + maxAcceptablePoints +"):"); // Balls rolled references how many balls were thrown before this, so if we're at 0 the ball we've just thrown is 1
        }

        // Prints the score board
        public static void WipeAndPrintNewScoreBoard(List<Frame> frames)
        {
            Console.Clear();
            PrintBorderLine(frames);
            PrintShotsLine(frames);
            PrintScoresLine(frames);
            PrintBorderLine(frames);
        }

        // Prints the top or bottom border lines
        private static void PrintBorderLine(List<Frame> frames)
        {
            string line = "";
            foreach (var frame in frames)
            {
                line += "*******";
            }
            Console.WriteLine(line + "*");
        }

        // Prints the line of the scoreboard that shows the shots on the frame
        private static void PrintShotsLine(List<Frame> frames)
        {
            string line = "*";

            foreach (var frame in frames)
            {
                switch (frame.GetBallsRolled())
                {
                    case 0:
                        if (frame is NormalFrame)
                            line += "   -|-*";
                        else
                            line += " -|-|-*";
                        break;
                    case 1:
                        if (frame is NormalFrame)
                            line += "   -|" + GetPrintableShot(frame, 0) + "*";
                        else
                            line += " -|-|" + GetPrintableShot(frame, 0) + "*";
                        break;
                    case 2:
                        if (frame is NormalFrame)
                            line += "   " + GetPrintableShot(frame, 1) + "|" + GetPrintableShot(frame, 0) + "*";
                        else
                            line += " -|" + GetPrintableShot(frame, 1) + "|" + GetPrintableShot(frame, 0) + "*";
                        break;
                    case 3:
                        line += " " + GetPrintableShot(frame, 2) + "|" + GetPrintableShot(frame, 1) + "|" + GetPrintableShot(frame, 0) + "*";
                        break;
                }
            }
            Console.WriteLine(line);
        }

        // Will convert our shot into what you see on the scoreboard (handles spares and strikes)
        public static string GetPrintableShot(Frame frame, int index)
        {
            List<int> shots = frame.Shots;
            int shot = shots.ElementAt(index);
            int ballsRolled = frame.GetBallsRolled();
            int previousShot = frame.GetPreviousShot(index);

            switch (ballsRolled)
            {
                case 1:
                    if (shot == 10)
                        return "X";
                    return Convert.ToString(shot);
                case 2:
                case 3:
                default:
                    if (shot + previousShot == 10 && index != 0)
                         return "/";
                    else if (shot == 10)
                        return "X";
                    return Convert.ToString(shot);
            }
        }

        // Prints the line of the scoreboard with the scores shown
        private static void PrintScoresLine(List<Frame> frames)
        {
            string line = "*";

            foreach (var frame in frames)
            {
                if (frame.FrameTotal == 0 || frame.StillCounting)
                {
                    line += "      ";
                }
                else if (frame.FrameTotal < 10)
                {
                    line += "     " + Convert.ToString(frame.FrameTotal);
                }
                else if (frame.FrameTotal >= 10 && frame.FrameTotal < 100)
                {
                    line += "    " + Convert.ToString(frame.FrameTotal);
                }
                else
                {
                    line += "   " + Convert.ToString(frame.FrameTotal);
                }
                line += "*";
            }

            Console.WriteLine(line);
        }

        // Will print the final score after the round has been finished
        public static void PrintFinalScore(int totalScore)
        {
            Console.WriteLine("YOUR FINAL SCORE IS: " + Convert.ToInt32(totalScore));
        }

    }
}
