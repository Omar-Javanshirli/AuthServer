using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace MiniApp1.API.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        //bir cox Policy yoxlansimiz oldugu zaman alt-alta atributlar yaza bileriy
        //[Authorize(Policy ="AgePolicy")]
        [Authorize(Roles = "admin",Policy ="BakiPolicy")]
        [HttpGet]
        public IActionResult GetStock()
        {

            var userName = HttpContext.User.Identity.Name;

            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            //veri tabanında  userId veya userName alanları üzerinden gerekli dataları çek

            // stockId stockQuantity  Category  UserId/UserName

            return Ok($"Stock işlemleri  =>UserName: {userName }- UserId:{userIdClaim.Value}");
        }
    }
}