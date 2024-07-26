using Microsoft.AspNetCore.Mvc;
using NumberToWordsAPI.Services;

namespace NumberToWordsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumberToWordsController(INumberToWordsService numberToWordsService) : ControllerBase
    {
        private readonly INumberToWordsService _numberToWordsService = numberToWordsService;

        [HttpGet("{number}")]
        public IActionResult Get(string number)
        {
            var result = _numberToWordsService.ConvertToWords(number);
            return Ok(result);
        }
    }
}
