namespace JaResult;

/// <summary>  
/// Represents the type of an error returned in a result.  
/// </summary>  
public enum ErrorType
{
	/// <summary>  
	/// Represents a general failure.  
	/// </summary>  
	Failure,

	/// <summary>  
	/// Represents a validation error.  
	/// </summary>  
	Validation,

	/// <summary>  
	/// Represents an unexpected error.  
	/// </summary>  
	Unexpected,

	/// <summary>  
	/// Represents a conflict error.  
	/// </summary>  
	Conflict,

	/// <summary>  
	/// Represents a not found error.  
	/// </summary>  
	NotFound,

	/// <summary>  
	/// Represents an unauthorized error.  
	/// </summary>  
	Unauthorized,

	/// <summary>  
	/// Represents a forbidden error.  
	/// </summary>  
	Forbidden,
}

