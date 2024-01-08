
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
 
    public class ErrorController : ControllerBase
    {
      [Route("/error")]
      public IActionResult Error()
      { 
        return Problem();  
      }
    }
}