using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GermanVocabApp.Api.VocabLists;

[ApiController]
[Route("api/vocab-lists")]
public class VocabListsController : ControllerBase
{
    private readonly IVocabListRepositoryAsync _repository;
    
    public VocabListsController(IVocabListRepositoryAsync repository)
    {
        _repository = repository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(VocabListResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create(CreateVocabListRequest request)
    {
        CreateVocabListDto dto = request.ToDto();
        VocabListDto newListDto = await _repository.Add(dto);
        VocabListResponse responseBody = newListDto.ToResponse();
        return CreatedAtAction(nameof(Get), new { id = newListDto.Id }, responseBody);
    }

    [HttpGet]
    public async Task<IActionResult> GetInfos()
    {
        IEnumerable<VocabListInfoDto> vocabLists = await _repository.GetVocabListInfos();
        IEnumerable<VocabListInfoResponse> responses = vocabLists.ToResponses();
        return Ok(responses);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(Guid id)
    {
        VocabListDto? dto = await _repository.Get(id);

        if (dto == null)
        {
            return NotFound();
        }
        VocabListResponse response = dto.ToResponse();
        return Ok(response);
    }
}