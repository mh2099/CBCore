namespace CBLib.Tools
{
    using System;

    public static class StringTools
    {
        public static string DeleteThis(this String value, String toDelete)
        {
            return value.Replace("\r\n", String.Empty).Replace(toDelete, String.Empty).Trim();
        }
        public static string DeleteLineBreakAndSpace(this String value)
        {
            return value.Replace("\r\n", String.Empty).Trim();
        }
    }
}