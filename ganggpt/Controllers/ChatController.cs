/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ganggpt.Services;
using ganggpt.Models;

using ganggpt.Middlewares;

namespace ganggpt.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]

    public class ChatController : ControllerBase
    {
        private readonly OpenAIService _openAIService;
        private readonly GangSlangMiddleware _gangSlangMiddleware;

        public ChatController(OpenAIService openAIService, GangSlangMiddleware gangSlangMiddleware)
        {
            _openAIService = openAIService;
            _gangSlangMiddleware = gangSlangMiddleware;
        }

        [HttpPost]
        public async Task<IActionResult> GetGangGPTResponse([FromBody] string inputText)
        {
            try
            {
                string chatGPTResponse = await _openAIService.GenerateResponseAsync(inputText);
                string gangGPTResponse = _gangSlangMiddleware.ProcessResponse(chatGPTResponse);
                return Ok(gangGPTResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       
    }
}
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ganggpt.Services;
using ganggpt.Models;

namespace ganggpt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly OpenAIService _openAIService;

        public ChatController(OpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost]
        public async Task<IActionResult> GetGangGPTResponse([FromBody] string inputText)
        {
            try
            {
                string chatGPTResponse = await _openAIService.GenerateResponseAsync(inputText);
                return Ok(chatGPTResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
