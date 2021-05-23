using ServiceEssupProject_Azure.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, String clave, object valor)
        {
            String data = ToolkitsService.SerializaObject(valor);

            session.SetString(clave, data);
        }
        public static T GetObject<T>(this ISession session, String clave)
        {
            string data = session.GetString(clave);

            if(data == null)
            {
                return default(T);
            }
            return ToolkitsService.DeserializaObject<T>(data);
        }
    }
}
