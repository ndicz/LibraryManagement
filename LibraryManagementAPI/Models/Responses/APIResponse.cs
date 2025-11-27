namespace LibraryManagementAPI.Models.Responses;

public class APIResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static APIResponse<T> Ok(T data, string? message = null)
        => new() { Success = true, Data = data, Message = message };

    public static APIResponse<T> Fail(string message)
        => new() { Success = false, Message = message };
}