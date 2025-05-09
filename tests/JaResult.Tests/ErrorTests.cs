namespace JaResult.Tests;
public class ErrorTests
{
	[Fact]
	public void Error_CreatedDirectly_HasCorrectValues()
	{
		var code = "123";
		var description = "Test error";
		var error = new Error(code, description, ErrorType.Failure);

		Assert.Equal(code, error.Code);
		Assert.Equal(description, error.Description);
		Assert.Equal(ErrorType.Failure, error.Type);
	}

	[Fact]
	public void Error_Failure_ReturnsDefaultFailure()
	{
		var error = Error.Failure();

		Assert.Equal("Failure", error.Code);
		Assert.Equal("A failure has occurred.", error.Description);
		Assert.Equal(ErrorType.Failure, error.Type);
	}

	[Fact]
	public void Error_NotFound_ReturnsNotFoundError()
	{
		var code = "VAL001";
		var descriptionn = "Not Found error";
		var error = Error.NotFound(code, descriptionn);

		Assert.Equal(code, error.Code);
		Assert.Equal(descriptionn, error.Description);
		Assert.Equal(ErrorType.NotFound, error.Type);
	}

	[Fact]
	public void Error_Validation_ReturnsValidationError()
	{
		var code = "VAL001";
		var descriptionn = "Field X is required";
		var error = Error.Validation(code, descriptionn);

		Assert.Equal(code, error.Code);
		Assert.Equal(descriptionn, error.Description);
		Assert.Equal(ErrorType.Validation, error.Type);
	}

	[Fact]
	public void Error_Conflict_ReturnsConflictError()
	{
		var code = "VAL001";
		var descriptionn = "Conflict Error";
		var error = Error.Conflict(code, descriptionn);

		Assert.Equal(code, error.Code);
		Assert.Equal(descriptionn, error.Description);
		Assert.Equal(ErrorType.Conflict, error.Type);
	}

	[Fact]
	public void Error_Custom_ReturnsUnauthorizedError()
	{
		var code = "VAL001";
		var descriptionn = "Unauthorized Error";
		var error = Error.Custom(code, descriptionn, ErrorType.Unauthorized);

		Assert.Equal(code, error.Code);
		Assert.Equal(descriptionn, error.Description);
		Assert.Equal(ErrorType.Unauthorized, error.Type);
	}

	[Fact]
	public void Error_ImplicitConversion_ToString_ReturnsCode()
	{
		Error error = new("E01", "Some error", ErrorType.Unexpected);
		string code = error;

		Assert.Equal("E01", code);
	}

}
