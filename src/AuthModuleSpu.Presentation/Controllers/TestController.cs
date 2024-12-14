using Microsoft.AspNetCore.Mvc;

namespace AuthModuleSpu.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("/test")]
    public string Test() => "Test";
}