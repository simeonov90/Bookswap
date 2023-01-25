namespace Bookswap.Application.Extensions.Methods
{
    public static class SupportedFileTypes
    {
        private static string[] supportedImageTypes = new string[] { "jpg", "jpeg", "png", "pdf", "gif" };

        public static bool IsValidImageType(this string fileExtension)
        {
            if (!supportedImageTypes.Contains(fileExtension.Substring(1))) return false;

            return true;
        }
    }
}
