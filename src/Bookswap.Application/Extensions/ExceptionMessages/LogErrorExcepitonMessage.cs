namespace Bookswap.Application.Extensions.ExceptionMessages
{
    public static class LogErrorExcepitonMessage
    {
        /// <summary>
        /// Something went wrong with in the {actionName}: ${exceptionMessage}"
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static string SomethingWentWrong(string actionName, string exceptionMessage) 
            => $"Something went wrong with in the {actionName}: ${exceptionMessage}";

    }
}
