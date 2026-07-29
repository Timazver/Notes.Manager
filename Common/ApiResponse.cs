namespace Notes.Manager.Common;

public sealed record ApiResponse<T>(int Status, T? Data = default, string? Error = null);
public static class ApiResponse
{
    public static ApiResponse<T> Success<T>(T? data, int status = 200)
    {
        return new ApiResponse<T>(status, data);
    }
    public static ApiResponse<T> Failure<T>(int status, string message)
    {
        return new ApiResponse<T>(status, default, message);
    }
}

