

namespace JaResult.Tests;

public class ResultTests
{
	[Fact]
	public void Success_CreatesResultWithValue()
	{
		var result = Result<string>.Success("test");

		Assert.False(result.HasError);
		Assert.True(result.IsSuccess);
		Assert.Equal("test", result.Value);
	}

	[Fact]
	public void Failure_WithSingleError_CreatesFailedResult()
	{
		var error = new Error("E001", "Failure", ErrorType.Failure);
		var result = Result<string>.Failure(error);

		Assert.True(result.HasError);
		Assert.False(result.IsSuccess);
		Assert.Single(result.Errors);
		Assert.Equal(error, result.FirstError);
	}

	[Fact]
	public void Failure_WithMultipleErrors_CreatesFailedResult()
	{
		var errors = new List<Error>
		{
			new("E001", "Error 1", ErrorType.Failure),
			new("E002", "Error 2", ErrorType.Validation)
		};

		var result = Result<string>.Failure(errors);

		Assert.True(result.HasError);
		Assert.Equal(2, result.Errors.Count);
		Assert.Equal(errors[0], result.FirstError);
	}

	[Fact]
	public void ImplicitConversion_FromValue_CreatesSuccessResult()
	{
		Result<string> result = "Hello";

		Assert.True(result.IsSuccess);
		Assert.False(result.HasError);
		Assert.Equal("Hello", result.Value);
	}

	[Fact]
	public void ImplicitConversion_FromSingleError_CreatesFailedResult()
	{
		Error error = new("ERR", "Something went wrong", ErrorType.Failure);
		Result<string> result = error;

		Assert.True(result.HasError);
		Assert.Single(result.Errors);
		Assert.Equal(error, result.FirstError);
	}

	[Fact]
	public void ImplicitConversion_FromListOfErrors_CreatesFailedResult()
	{
		var errors = new List<Error> { new("1", "e", ErrorType.Validation) };
		Result<string> result = errors;

		Assert.True(result.HasError);
		Assert.Equal(errors[0], result.FirstError);
	}

	[Fact]
	public void FirstError_ThrowsIfNoErrors()
	{
		var result = Result<string>.Success("ok");

		var ex = Assert.Throws<InvalidOperationException>(() => _ = result.FirstError);
		Assert.Equal("Cannot access FirstError when there are no errors.", ex.Message);
	}

	[Fact]
	public void Value_ThrowsIfHasError()
	{
		Result<string> result = new Error("X", "Fail", ErrorType.Failure);

		var ex = Assert.Throws<InvalidOperationException>(() => _ = result.Value);
		Assert.Equal("Cannot access Value when the result has errors.", ex.Message);
	}

	[Fact]
	public void Match_ExecutesOnValue_WhenSuccess()
	{
		var result = Result<int>.Success(1);
		var output = result.Match(
			onValue: val => val + 1,
			onError: (List<Error>? errs) => 0
		);

		Assert.Equal(2, output);
	}

	[Fact]
	public void Match_ExecutesOnError_WhenFailure_ListOverload()
	{
		Result<int> result = new List<Error> { new("ERR", "fail", ErrorType.Failure) };
		var output = result.Match(
			onValue: _ => 1,
			onError: errs => errs?.Count
		);

		Assert.Equal(1, output);
	}

	[Fact]
	public void Match_ExecutesOnError_WhenFailure_FirstErrorOverload()
	{
		Result<int> result = new Error("ERR", "fail", ErrorType.Failure);
		var output = result.Match(
			onValue: _ => 1,
			onError: err => err.Code == "ERR" ? 2 : 0
		);

		Assert.Equal(2, output);
	}
}
