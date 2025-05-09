namespace JaResult;

/// <summary>
/// Represents the type of an error returned in a result.
/// </summary>
public enum ErrorType
{
	Failure,
	Validation,
	Unexpected,
	Conflict,
	NotFound,
	Unauthorized,
	Forbidden,
}

