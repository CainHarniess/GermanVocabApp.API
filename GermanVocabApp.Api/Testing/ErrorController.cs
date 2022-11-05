using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GermanVocabApp.Api.Testing;

[ApiController]
[Route("api/v1/testing/errors")]
public class ErrorController : ControllerBase
{

    [HttpGet("no-content")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult ReturnNoContent()
    {
        return NoContent();
    }

    [HttpGet("unauthorised")]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public IActionResult ThrowUnauthorised()
    {
        return Unauthorized();
    }

    [HttpPost("bad-request")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult ThrowBadRequest()
    {
        return BadRequest();
    }
}
