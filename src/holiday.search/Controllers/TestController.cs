using Microsoft.AspNetCore.Mvc;

namespace holiday.search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
            
        }

        [HttpGet]
        public bool Get()
        {
            return true;
        }
    }
}