using BowlingConsoleApp.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingConsoleApp.Helpers
{
    // This is a helper class to make sure the users inputs are valid
    public static class InputHelper
    {
       
        public static int GetPinsHit(string currentBall, Ball previousBall = null) // Auto initialize the ball to null if there isn't one provided
        {
            int pinsHit = -1;                                   
            bool validInput = false;

            Console.WriteLine("Please enter how many pins were hit with " + currentBall + " ball:");

            while (!validInput) 
            {                       
                pinsHit = CheckIfInputIsInteger(Console.ReadLine());                                        // Read user input, check if it's valid
                
                if (pinsHit == -1)                                                                          // If it's still -1, it's invalid and we continue
                {
                    Console.WriteLine("User input is not valid integer (0-10). Try again.");                     
                    continue;
                }

                validInput = VerifyPinTotalAmount(pinsHit, previousBall);                                   // We can exit the loop if we get here
            }

            return pinsHit;                                                                                 // Return our valid input for the amount of pins hit
        }

        public static int CheckIfInputIsInteger(string userInput)                                       // This will make sure that it's a valid integer3
        {
            int value = -1;
            if (!String.IsNullOrEmpty(userInput))
            {
                if (!int.TryParse(userInput, out value)) 
                {                                     
                    value = -1;     // If it's not an integer, we want to set it back to -1, because tryparse will set it to 0 if it's not a valid integer (0 is valid)
                }
            }
            return value;
        }

        public static bool VerifyPinTotalAmount(int? input, Ball previousBall = null)
        {
            if (input is null)
            {
                return false;
            }

            if (previousBall is null)                                                                   // If it's the first ball
            {
                if (input > 10 || input < 0)                                                            // Make sure it's between 0 and 10
                {
                    Console.WriteLine("User input must be between (0-10). Try again.");                 // If it's not tell the user
                    return false;
                }
            }
            else
            {                                                                                           // Otherwise
                if (previousBall.PinsHit + input > 10)                                                  // Make sure that this ball and the previous do not exceed 10
                {
                    Console.WriteLine("User cannot exceed a total of 10 pins per round. Try again.");   // If it does tell the user
                    return false;
                }
                else if (input < 0)                                                                      // Check if it's below 0
                {
                    Console.WriteLine("User input musn't be negative. Try again.");                      // If it is, tell the user
                    return false;
                }
            }

            return true;
        }
    }
}
