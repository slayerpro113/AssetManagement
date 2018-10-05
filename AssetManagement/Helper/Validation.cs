using System;

namespace AssetManagement.Helper
{
    public static class Validation
    {
        public static bool ValidateInput(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            return true;
        }
    }
}