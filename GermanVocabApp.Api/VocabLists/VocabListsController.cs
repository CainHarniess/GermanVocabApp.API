using FluentValidation.Results;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GermanVocabApp.Api.VocabLists;

[ApiController]
[Route("api/vocab-lists")]
[Authorize]
public class VocabListsController : ControllerBase
{
    private readonly IVocabListRepositoryAsync _repository;
    private readonly IValidationController<ListRequest> _validator;
    private readonly IConverter<VocabListDto, ListResponse> _responseConverter;
    private readonly IConverter<VocabListInfoDto[], ListInfoResponse[]> _infoResponseConverter;
    private readonly IConverter<ListRequest, VocabListDto> _createRequestConverter;
    private readonly IUpdateResourceConverter<ListRequest, VocabListDto> _updateRequestConverter;

    public VocabListsController(IValidationController<ListRequest> validator,
        IVocabListRepositoryAsync repository,
        IConverter<VocabListDto, ListResponse> responseConverter,
        IConverter<VocabListInfoDto[], ListInfoResponse[]> infoResponseConverter,
        IConverter<ListRequest, VocabListDto> createRequestConverter,
        IUpdateResourceConverter<ListRequest, VocabListDto> updateRequestConverter)
    {
        _validator = validator;
        _repository = repository;
        _responseConverter = responseConverter;
        _infoResponseConverter = infoResponseConverter;
        _createRequestConverter = createRequestConverter;
        _updateRequestConverter = updateRequestConverter;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ListResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Create(ListRequest request)
    {
        ValidationResult result = _validator.Validate(request);
        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }

        if (request.Id != null)
        {
            return BadRequest("List ID may not be provided on list creation.");
        }

        VocabListDto dto = _createRequestConverter.Convert(request);
        VocabListDto newListDto = await _repository.Add(dto);

        if (newListDto.Id == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected null primary key on created resource.");
        }

        ListResponse responseBody = _responseConverter.Convert(newListDto);
        return CreatedAtAction(nameof(Get), new { id = newListDto.Id }, responseBody);
    }

    [HttpGet]
    public async Task<IActionResult> GetInfos()
    {
        IEnumerable<VocabListInfoDto> vocabLists = await _repository.GetVocabListInfos();
        IEnumerable<ListInfoResponse> responses = _infoResponseConverter.Convert(vocabLists.ToArray());
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
        ListResponse response = _responseConverter.Convert(dto);
        return Ok(response);
    }

    [HttpPut(ActionParameters.IdGuid)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateVocabList(Guid id, ListRequest request)
    {
        ValidationResult result = _validator.Validate(request);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        VocabListDto updateDto = _updateRequestConverter.Convert(request, id);
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
}
