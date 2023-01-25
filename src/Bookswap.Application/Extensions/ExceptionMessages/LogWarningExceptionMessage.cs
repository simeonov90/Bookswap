using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bookswap.Application.Extensions.ExceptionMessages
{
    public static class LogWarningExceptionMessage
    {
        public static string EntityRecordDoesNotExists<TKey>(string actionName, TKey id) => $"{actionName} - Entity record with id={id} does not exists.";
        public static string UpdateParametersAreNotSame<TKey>(string actionName, TKey entityId, TKey modelEntityId) => $"{actionName} - Given entity id={entityId} is not equal to the model entity ${modelEntityId}!";
    }
}
