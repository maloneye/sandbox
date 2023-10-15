namespace BuinessLogic
{
    public static class Extensions
    {

        public static string ToShort(this string emoji)
        {
            return EmojiOne.EmojiOne.ToShort(emoji);
        }

        public static string ToImage(this string emoji)
        {
            return EmojiOne.EmojiOne.ShortnameToUnicode(emoji);
        }
    }
}
