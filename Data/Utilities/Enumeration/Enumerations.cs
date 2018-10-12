namespace Data.Utilities.Enumeration
{
    public class Enumerations
    {
        public enum Roles
        {
            Admin,
            Manager,
            User
        }

        public enum LoginStatus
        {
            WrongUserName,
            WrongPassword,
            Succsess
        }

        public enum CategoryName
        {
            Chair,
            Keyboard,
            Mouse,
            PC,
            Printer,
            Screen
        }

        public enum AddEntityStatus
        {
            Success,
            Failed
        }

        public enum UpdateEntityStatus
        {
            Success,
            Failed
        }
    }
}
