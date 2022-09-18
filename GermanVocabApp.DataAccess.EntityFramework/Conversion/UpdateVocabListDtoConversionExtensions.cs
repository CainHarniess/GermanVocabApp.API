using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Conversion;

internal static class UpdateVocabListDtoConversionExtensions
{
    public static void CopyListDetails(this UpdateVocabListDto updateDto,
                                       VocabList entity, DateTime updateTimestamp)
    {
        entity.Name = updateDto.Name;
        entity.Description = updateDto.Description;
        entity.UpdatedDate = updateTimestamp;
    }
}
