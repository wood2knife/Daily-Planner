﻿using Daily_Planner.Domain.Dto.Report;
using FluentValidation;

namespace Daily_Planner.Application.Validations.FluentValidations.Report;

public class UpdateReportValidator : AbstractValidator<UpdateReportDto>
{
    public UpdateReportValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
    }
}