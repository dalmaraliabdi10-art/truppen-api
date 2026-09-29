namespace TruppenApi.Services;

public record FileSaveResult(bool Success, string? RelativePath, string? Error);

public interface IFileStorageService
{
    Task<FileSaveResult> SaveImageAsync(IFormFile file);
}

// IFileStorageService separerar filhantering från spelardatahantering vilket gör koden mer modulär och testbar. 
// FileSaveResult gör att tjänsten kan returnera antingen en sökväg eller ett felmeddelande
// så att PlayersController kan välja rätt statuskod.