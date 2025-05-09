namespace JaResult;

/// <summary>
/// Represents an error with a code, description, and error type.
/// </summary>
/// <param name="Code">The error code identifier.</param>
/// <param name="Description">A human-readable description of the error.</param>
/// <param name="Type">The type/category of the error.</param>
public record struct Error(string Code, string Description, ErrorType Type)
{
	/// <summary>
	/// Creates a generic failure error.
	/// </summary>
	public static Error Failure(string code = "Failure", string description = "A failure has occurred.") =>
		new(code, description, ErrorType.Failure);

	/// <summary>
	/// Creates a not found error.
	/// </summary>
	public static Error NotFound(string code = "NotFound", string description = "The resource was not found.") =>
		new(code, description, ErrorType.NotFound);

	/// <summary>
	/// Creates a validation error.
	/// </summary>
	public static Error Validation(string code = "Validation", string description = "Validation failed.") =>
		new(code, description, ErrorType.Validation);

	/// <summary>
	/// Creates a conflict error.
	/// </summary>
	public static Error Conflict(string code = "Conflict", string description = "A conflict occurred.") =>
		new(code, description, ErrorType.Conflict);

	/// <summary>
	/// Creates a custom error with specific values.
	/// </summary>
	public static Error Custom(string code, string description, ErrorType errorType) =>
		new(code, description, errorType);

	/// <summary>
	/// Converts an error to its code string representation.
	/// </summary>
	public static implicit operator string(Error error) => error.Code ?? string.Empty;
}
