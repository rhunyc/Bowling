using Bowling_Console_App.Game.Contracts;
using Bowling_Console_App.Game.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling_Console_App.Game
{
#nullable enable
    public class LastFrame : Frame
    {
        override public int PossibleRolls { get; set; } = 3;

        // Our logic that happens per ball is rolled -- exactly the same as our normal frame but we don't need to track the score
        override public FrameScoreUpdater? Roll()
        {
            OutputHelper.PromptUserForPinsHit(GetBallsRolled());
            while (true)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int num))
                {
                    int inputPoints = int.Parse(userInput);

                    if (ValidatePoints(inputPoints))
                    {
                        AddPointsToFrameScore(inputPoints);
                        Shots.Add(inputPoints);
                        break;
                    }
                }
                OutputHelper.PromptUserForValidInput(GetPreviousShot(GetBallsRolled()));
            }

            return null;
        }

        // Determines whether or not the frame can be played again
        override public bool CanBePlayed()
        {
            switch (GetBallsRolled())
            {
                case 0:
                case 1:
                    break;
                case 2:
                    if (Shots.ElementAt(0) + Shots.ElementAt(1) < 10)
                        return false;
                    break;
                case 3:
                    return false;
            }
            return true;
        }

        // Validates the input from user to make sure it's a legal amount
        override public bool ValidatePoints(int input)
        {
            int ballsRolled = GetBallsRolled();
            int previousShot = GetPreviousShot(ballsRolled);
            switch (ballsRolled)
            {
                case 0:
                    if (InputHelper.InputBetween0and10(input))
                        return true;
                    break;
                case 1:
                    if (previousShot < 10 && InputHelper.InputAndPreviousShotValid(input, previousShot))
                        return true;
                    if (previousShot == 10 && InputHelper.InputBetween0and10(input))
                        return true;
                    break;
                case 2:
                    if (previousShot < 10 && InputHelper.InputAndPreviousShotValid(input, previousShot))
                        return true;
                    else if (previousShot == 10 && InputHelper.InputBetween0and10(input))
                        return true;
                    break;
                default:
                    return false;
            }
            return false;
        }

        // Gets our previous shot
        override public int GetPreviousShot(int indexOfCurrentShot)
        {
            if (indexOfCurrentShot == 0)
                return 0;
            else if (indexOfCurrentShot > 3)
                return 0;
            
            return Shots.ElementAt(indexOfCurrentShot - 1);
        }
    }
}
