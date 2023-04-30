using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ganggpt.Middlewares
{
    public class GangSlangMiddleware
    {
        public string ProcessResponse(string response)
        {
            string[] slangDictionary = { "homie", "crew", "ride", "chillin'" };
            string[] normalWords = { "friend", "group", "car", "relaxing" };

            for (int i = 0; i < normalWords.Length; i++)
            {
                response = response.Replace(normalWords[i], slangDictionary[i]);
            }

            return response;
        }
    }
}