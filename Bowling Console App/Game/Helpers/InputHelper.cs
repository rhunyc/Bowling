using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling_Console_App.Game.Helpers
{
    public static class InputHelper
    { 
        // Makes sure the input is between 0 and 10
        public static bool InputBetween0and10(int input)
        {
            return (input >= 0 && input <= 10);
        }

        // Makes sure that the input and previous shot do not exceed 10, and the input is not negative
        public static bool InputAndPreviousShotValid(int input, int previousShot)
        {
            return (input >= 0) && (input + previousShot <= 10);
        }
    }
}
