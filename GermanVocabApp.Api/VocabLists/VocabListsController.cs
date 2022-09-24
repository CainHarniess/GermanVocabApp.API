using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.Core.Validation.DependencyInjection;
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
    private readonly IValidator<CreateVocabListRequest> _creationValidator;
    private readonly IValidator<UpdateVocabListRequest> _updateValidator;

    public VocabListsController(IValidator<CreateVocabListRequest> creationValidator,
        IValidator<UpdateVocabListRequest> updateValidator,
        IVocabListRepositoryAsync repository)
    {
        _creationValidator = creationValidator;
        _updateValidator = updateValidator;
        _repository = repository;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(VocabListResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Create(CreateVocabListRequest request)
    {
        IValidationResult result = _creationValidator.Validate(request);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        CreateVocabListDto dto = request.ToDto();
        VocabListDto newListDto = await _repository.Add(dto);

        try
        {
            ValidateCreatedDto(newListDto);
        }
        catch (UnexpectedNullIdException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

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

    [HttpGet(ActionParameters.IdGuid)]
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
    
    [HttpPut(ActionParameters.IdGuid)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateVocabList(Guid id, UpdateVocabListRequest request)
    {
        IValidationResult result = _updateValidator.Validate(request);

        if (!result.IsValid)
        {
            return BadRequest();
        }

        UpdateVocabListDto updateDto = request.ToDto(id);
        try
        {
            await _repository.Update(updateDto);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (UnexpectedIdException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        return NoContent();
    }

    [HttpDelete(ActionParameters.IdGuid)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> HardDelete(Guid id)
    {
        bool result = await _repository.HardDelete(id);
        
        if (result == false)
        {
            return NotFound();
        }
        return NoContent();
    }


    private void ValidateCreatedDto(VocabListDto newListDto)
    {
        if (newListDto.Id == null)
        {
            throw new UnexpectedNullIdException($"Instance of {nameof(VocabListDto)} created with a null ID when an ID is expected.");
        }
    }


    private static class ActionParameters
    {
        public const string IdGuid = "{id:guid}";
    }
}