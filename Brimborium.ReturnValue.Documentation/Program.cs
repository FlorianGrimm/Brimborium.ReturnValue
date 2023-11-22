namespace Brimborium.ReturnValue;

internal partial class Program {
    static void Main(string[] args) {
        Console.WriteLine("Hello, World!");
        string? path = @"c:\temp\gna";

        var ucPath = path.AsUncheckedValue();

        //Result<string?> pa
        //if (string.IsNullOrEmpty(ucPath.Value)) {
        //}

        //Result<FilePath> rfp = new Result<FilePath>(new FilePath(""));
        //FilePath filePath = new FilePath("");
        /*
        if (rfp.TryGetError(out var errorValue, out var successValue)) {
        } else { 
        }
        */
        FilePath fpSrc = new("x");
        FilePath fpDst = new();
        System.Console.Out.WriteLine("fpSrc");
        System.Console.Out.WriteLine(fpSrc.Value ?? "-");
        System.Console.Out.WriteLine("fpDst");
        System.Console.Out.WriteLine(fpDst.Value ?? "-");
        //System.Console.Out.WriteLine(fpDst.TryGetValue(out var _));
        //fpDst.TryGetValue
        //var x = ((IMeaningReference<string>)fpDst);
        //    x.TryGetValue
    }
}

//public interface IDirectoryExists {
//    Result<bool> ResultValue { get; }

//    //void Deconstruct(out Result<bool> ResultValue);
//    //bool Equals(DirectoryExists other);
//    //bool Equals(object obj);
//    //int GetHashCode();
//    //string ToString();
//}

//public record struct DirectoryExists(Result<bool> Result) : IWrappedResult<bool> {
//    public static Result<DirectoryExists> ExecuteResult(Result<FilePath> rfPath) {
//        if (rfPath.TryGetError(out var errorValue)) {
//            return errorValue;
//        } else if (rfPath.TryGetValue(out var filePath)) {
//            return DirectoryExists.Execute(filePath);
//        } else {
//            return new ErrorValue(new InvalidCaseException());
//        }
//    }

//    public static DirectoryExists Execute(FilePath path) {
//        try {
//            return new(System.IO.Directory.Exists(path.Value));
//        } catch (Exception error) {
//            return new(ErrorValue.CreateFromCatchedException(error));
//        }
//    }
//}