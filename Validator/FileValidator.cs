
using trainee_management.Exceptions;

public class FileValidator()
{
    public static readonly List<string> validExtensions = [".zip", ".pdf"];
    static public void validateFile(IFormFile file)
    {

        
        //filesize
        long size = file.Length;
        if (size > (20 * 1024 * 1024)) throw new FileSizeException("File size should not be greater than 20 mb");
        if(size==0) throw new FileSizeException("Invalid File Size");
        
        if (file == null)
        {
            throw new ValidationException("File is null");
        }
        string extension = Path.GetExtension(file.FileName.ToLower());
        if (!validExtensions.Contains(extension))
        {
            throw new InvalidExtensionException($"File extension is invalid. only {string.Join(",", validExtensions)} files are allowed");
        }
    }

}