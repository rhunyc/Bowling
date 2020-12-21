using Bowling_Console_App.Game.Contracts;
using Bowling_Console_App.Game.Helpers;
using System;
using System.Linq;

namespace Bowling_Console_App.Game
{
    #nullable enable
    public class NormalFrame : Frame
    {
        override public int PossibleRolls { get; set; } = 2;

        // Our logic that happens per ball is rolled
        override public FrameScoreUpdater? Roll()
        {
            FrameScoreUpdater? frameScoreUpdater = null;                                    // Initialize our framescoreupdater as null (so we can return / assign it later)
            OutputHelper.PromptUserForPinsHit(GetBallsRolled());                            // Writes the prompt for the user to input pins hit for whichever ball is being rolled
            while (true)                                                                    // Loop until we get valid points
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int num))                                   // Try to get a valid integer from what the user types
                {
                    int inputPoints = int.Parse(userInput);

                    if (ValidatePoints(inputPoints))                                        // Check to make sure that the int they entered is a good amount
                    {
                        Shots.Add(inputPoints);                                             // If it is, we want to add it
                        AddPointsToFrameScore(inputPoints);                                 // We also want to add the points to our frames current score
                        frameScoreUpdater = CreateFrameScoreUpdaterAndTrackFrame();         // Determine whether or not to create a score tracker and track the frame
                        break;
                    }
                }
                OutputHelper.PromptUserForValidInput(GetPreviousShot(GetBallsRolled()));    // If the input isn't valid, remind the user to put in the right value
            }

            return frameScoreUpdater;                                                       // Return our frame tracker if it's created
        }

        // Determines whether or not the frame can be played again
        override public bool CanBePlayed()
        {
            if (GetBallsRolled() < PossibleRolls && FrameScore < 10)
                return true;
            
            return false;
        }
        
        // Validates the input from user to make sure it's a legal amount
        override public bool ValidatePoints(int input)
        {
            int ballsRolled = GetBallsRolled();

            if (ballsRolled == 0 && InputHelper.InputBetween0and10(input))
                return true;
            
            if (ballsRolled == 1 &&  InputHelper.InputAndPreviousShotValid(input, GetPreviousShot(1)))
                return true;
            
            return false;
        }

        // Checks if the conditions are right and if they are it creates our tracker and updates the StillCounting value for tracking the frame
        public FrameScoreUpdater? CreateFrameScoreUpdaterAndTrackFrame() {
            int ballsRolled = GetBallsRolled();

            if (FrameScore == 10)
            {
                if (ballsRolled == 1)
                {
                    StillCounting = true;
                    return new FrameScoreUpdater() { Frame = this, RollCounter = 2 };
                }
                else if (ballsRolled == 2)
                {
                    StillCounting = true;
                    return new FrameScoreUpdater() { Frame = this, RollCounter = 1 };
                }
            }

            return null;
        }


        // Gets our previous shot
        override public int GetPreviousShot(int indexOfCurrentShot)
        {
            if (indexOfCurrentShot <= 0 || indexOfCurrentShot > 1)
                return 0;

            return Shots.ElementAt(indexOfCurrentShot - 1);
        }
    }
}
