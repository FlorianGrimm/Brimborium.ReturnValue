using Brimborium.ReturnValue;

namespace Brimborium.Meaning.Wrapped;

public sealed record FilePath(string Value) : IValueMeaning<string>;

public sealed record SearchPattern(string Value) : IValueMeaning<string>;

public interface IFileSystemService {
    Result<bool> DirectoryExists(        
        FilePath filePath);

    Result<FileSystemInfo[]> GetFileSystemInfos(
        FilePath filePath, 
        SearchPattern searchPattern, 
        SearchOption searchOption);
}

public class FileSystemService : IFileSystemService {
    public FileSystemService() { }

    public Result<bool> DirectoryExists(FilePath filePath) {
        try {
            var di = new System.IO.DirectoryInfo(filePath.Value);
            var result = di.Exists;
            return result;
        } catch (Exception error) {
            return ErrorValue.CreateFromCatchedException(error);
        }
    }

    public Result<FileSystemInfo[]> GetFileSystemInfos(
        FilePath filePath,
        SearchPattern searchPattern,
        SearchOption searchOption) {
        try {
            var di = new System.IO.DirectoryInfo(filePath.Value);
            var result = di.GetFileSystemInfos(searchPattern.Value, searchOption);
            return result;
        } catch (Exception error) {
            return ErrorValue.CreateFromCatchedException(error);
        }
    }
}
