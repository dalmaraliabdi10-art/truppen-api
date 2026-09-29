namespace TruppenApi.Services;

// Guid ger unika filnamn, vilket löser två saker: två uppladdningar med samma namn krockar
// inte och ett filnamn från klienten kan inte användas för att skriva utanför uploads-mappen.
// Det är sökvägen som sparas i databasen inte själva filen.
// Statiska filer serveras från wwwroot så bilden kan hämtas direkt via webbläsaren.
// MaxBytes begränsar filstorleken och AllowedExtensions begränsar filtyperna som kan laddas upp.
public class FileStorageService : IFileStorageService
{
    private const string UploadFolder = "uploads";
    private const long MaxBytes = 5 * 1024 * 1024;
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

    private readonly IWebHostEnvironment _env;

    public FileStorageService(IWebHostEnvironment env) => _env = env;

    public async Task<FileSaveResult> SaveImageAsync(IFormFile file)
    { // Validerar filen innan den sparas. Om filen är tom, för stor eller har en otillåten filtyp returneras ett felmeddelande.
        if (file.Length == 0)
            return new FileSaveResult(false, null, "Filen är tom.");

        if (file.Length > MaxBytes)
            return new FileSaveResult(false, null,
                $"Filen är för stor. Högst {MaxBytes / (1024 * 1024)} MB tillåts.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
            return new FileSaveResult(false, null,
                $"Filtypen {extension} stöds inte. Tillåtna: {string.Join(", ", AllowedExtensions)}.");

        var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        var targetFolder = Path.Combine(webRoot, UploadFolder);
        Directory.CreateDirectory(targetFolder);

        var fileName = $"{Guid.NewGuid():N}{extension}";
        var fullPath = Path.Combine(targetFolder, fileName);

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return new FileSaveResult(true, $"/{UploadFolder}/{fileName}", null);
    }
}