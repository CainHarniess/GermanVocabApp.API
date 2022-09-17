using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Create(CreateVocabListRequest request)
    {
        CreateVocabListDto dto = request.ToDto();
        Guid newListId = await _repository.Add(dto);
        return CreatedAtAction(nameof(Get), new { id = newListId }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetInfos()
    {
        IEnumerable<VocabListInfoDto> vocabLists = await _repository.GetVocabListInfos();
        IEnumerable<VocabListInfoResponse> responses = vocabLists.ToResponses();
        return Ok(responses);
    }

    [HttpGet("{id:guid}")]
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
