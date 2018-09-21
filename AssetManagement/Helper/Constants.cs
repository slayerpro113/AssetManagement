namespace AssetManagement.Helper
{
    public static class Constants
    {
        public static string USER_SESSION = "USER_SESSION";
    }

    public enum LoginStatus
    {
        WrongUserName,
        WrongPassword,
        Successful
    }
}