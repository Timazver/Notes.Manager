namespace Notes.Manager.Common;

public sealed record ApiPageResponse<T>(List<T> Items, int Page, int Size, int TotalElements, int TotalPages);
