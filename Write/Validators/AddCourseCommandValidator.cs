using System;

namespace Validators
{
	using Domain.Commands;

	using FluentValidation;
	using Infrastructure.Validator.Contract;
	using Infrastructure.Validator.Utility;

	public class AddCourseCommandValidator : AbstractValidator<AddCourseCommand>, Infrastructure.Validator.Contract.IValidator<AddCourseCommand>
	{
		public AddCourseCommandValidator()
		{
			RuleFor(p => p.CourseGuid).NotEmpty();
			RuleFor(p => p.Teacher).NotEmpty();
			RuleFor(p => p.MaxParticipants).NotEmpty().GreaterThan(0);
			RuleFor(p => p.Name).NotEmpty();
		}
		public ValidationResult PerformValidation(AddCourseCommand command) =>
			Validate(command).ToValidationResult();
	}
	
}
