namespace Bookswap.Application.Extensions.ExceptionMessages
{
    public static class CommonExceptionMessage
    {
        /// <summary>
        /// Something went wrong.
        /// </summary>
        public static string SomethingWentWron
            => "Something went wrong.";

        /// <summary>
        /// Something went wrong in the {actionName}."
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static string SomethingWentWrong(string actionName)
            => $"Something went wrong in the {actionName}.";

        /// <summary>
        /// Something went wrong in the {actionName}. Please contact support.
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static string SomethingWentWrongContactSupport(string actionName)
            => $"Something went wrong in the {actionName}. Please contact support.";
    }
}
