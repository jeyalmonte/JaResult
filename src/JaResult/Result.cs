namespace JaResult;

/// <summary>
/// Represents the result of an operation, which can be either a success with a value or a failure with errors.
/// </summary>
public readonly record struct Result<TValue> : IResult<TValue>
{
	/// <inheritdoc/>
	public TValue Value =>
		!HasError
			? _value!
			: throw new InvalidOperationException("Cannot access Value when the result has errors.");

	/// <inheritdoc/>
	public bool HasError { get; }

	/// <inheritdoc/>
	public List<Error> Errors => HasError
		? _errors ?? new List<Error>()
		: new List<Error>();

	/// <summary>
	/// Indicates whether the result represents a successful operation.
	/// </summary>
	public bool IsSuccess => !HasError;


	/// <summary>
	/// Gets the first error when the result has errors. Throws if there are no errors.
	/// </summary>
	public Error FirstError =>
		HasError && _errors?.Count > 0
			? _errors[0]
			: throw new InvalidOperationException("Cannot access FirstError when there are no errors.");

	// ---------- Factory Methods ----------

	/// <summary>
	/// Creates a successful result with the given value.
	/// </summary>
	public static Result<TValue> Success(TValue value) =>
		new(value: value);

	/// <summary>
	/// Creates a failed result with the given errors.
	/// </summary>
	public static Result<TValue> Failure(params Error[] errors) =>
		new(errors: new List<Error>(errors), hasError: true);

	/// <summary>
	/// Creates a failed result with the given list of errors.
	/// </summary>
	public static Result<TValue> Failure(List<Error> errors) =>
		new(errors: errors, hasError: true);

	// ---------- Implicit Conversions ----------

	/// <summary>
	/// Implicitly creates a successful result from a value.
	/// </summary>
	public static implicit operator Result<TValue>(TValue value) =>
		Success(value);

	/// <summary>
	/// Implicitly creates a failed result from a single error.
	/// </summary>
	public static implicit operator Result<TValue>(Error error) =>
		Failure(error);

	/// <summary>
	/// Implicitly creates a failed result from an array of errors.
	/// </summary>
	public static implicit operator Result<TValue>(Error[] errors) =>
		Failure(errors);

	/// <summary>
	/// Implicitly creates a failed result from a list of errors.
	/// </summary>
	public static implicit operator Result<TValue>(List<Error> errors) =>
		Failure(errors);


	/// <summary>
	/// Matches the result and invokes the appropriate function based on success or failure.
	/// </summary>
	/// <typeparam name="TResult">The type of the return value.</typeparam>
	/// <param name="onValue">Function to invoke if the result is successful.</param>
	/// <param name="onError">Function to invoke if the result has errors.</param>
	/// <returns>The result of the invoked function.</returns>
	public TResult Match<TResult>(
		Func<TValue, TResult> onValue,
		Func<List<Error>?, TResult> onError)
	{
		return HasError
			? onError(Errors)
			: onValue(Value);
	}


	/// <summary>
	/// Matches the result and invokes the appropriate function based on success or failure.
	/// </summary>
	/// <typeparam name="TResult">The type of the return value.</typeparam>
	/// <param name="onValue">Function to invoke if the result is successful.</param>
	/// <param name="onError">Function to invoke if the result has errors.</param>
	/// <returns>The result of the invoked function.</returns>
	public TResult Match<TResult>(
		Func<TValue, TResult> onValue,
		Func<Error, TResult> onError)
	{
		return HasError
			? onError(FirstError)
			: onValue(Value);
	}

	private readonly List<Error>? _errors;
	private readonly TValue? _value;
	private Result(TValue? value = default, List<Error>? errors = null, bool hasError = false)
	{
		_value = value;
		_errors = errors;
		HasError = hasError;
	}
}
