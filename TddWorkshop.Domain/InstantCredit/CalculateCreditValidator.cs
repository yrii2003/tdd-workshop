using FluentValidation;

namespace TddWorkshop.Domain.InstantCredit;

public class CalculateCreditValidator: AbstractValidator<CalculateCreditRequest>
{
    public CalculateCreditValidator()
    {
    }
}


public class PersonalInfoValidator: AbstractValidator<PersonalInfo>
{
    public PersonalInfoValidator() 
    {
        RuleFor(x => x.Age).InclusiveBetween(18, 60);
    }
}

public class PassportInfoValidator: AbstractValidator<PassportInfo>
{
    public PassportInfoValidator()
    {
        RuleFor(x => x.Series).Length(4, 4);
        RuleFor(x => x.Number).Length(6, 6);
        RuleFor(x => x.IssueDate).LessThan(DateTime.Today);
    }
}

public class CreditInfoValidator: AbstractValidator<CreditInfo>
{
    public CreditInfoValidator() 
    {
        RuleFor(x => x.Sum).InclusiveBetween(10_000, 10_000_000);
    }
}