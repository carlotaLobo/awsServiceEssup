using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Helpers
{
    public class ToolkitsService
    {
        public static String SerializaObject(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T DeserializaObject<T>(String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    
    }
}
