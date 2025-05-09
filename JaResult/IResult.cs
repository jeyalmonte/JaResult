namespace JaResult;

/// <summary>
/// Represents the result of an operation, with potential errors.
/// </summary>
public interface IResult
{
	/// <summary>
	/// Gets a List of errors if the operation failed.
	/// </summary>
	List<Error>? Errors { get; }

	/// <summary>
	/// Indicates whether the result contains any errors.
	/// </summary>
	bool HasError { get; }
}

/// <summary>
/// Represents the result of an operation that returns a value.
/// </summary>
/// <typeparam name="TValue">The type of the value returned by the operation.</typeparam>
public interface IResult<out TValue> : IResult
{
	/// <summary>
	/// Gets the value of the result if the operation succeeded.
	/// </summary>
	TValue Value { get; }
}
