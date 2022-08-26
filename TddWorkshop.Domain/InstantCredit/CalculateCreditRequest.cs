using MediatR;

namespace TddWorkshop.Domain.InstantCredit;

public record CalculateCreditRequest (
    PersonalInfo PersonalInfo,
    CreditInfo CreditInfo,
    PassportInfo PassportInfo
): IRequest<CalculateCreditRespons>;

public record PassportInfo(string Series, string Number, DateTime IssueDate, string IssuedBy);

public record PersonalInfo([property: KeyValues(21, 28, 29, 59, 60, 72)]int Age, string FirstName, 
    string LastName, string? Patronymic = null);

public record CreditInfo(CreditGoal Goal, 
    [property: KeyValues(10_000, 1_000_000, 3_000_000, 5_000_000, 10_000_000)] decimal Sum, 
    Deposit Deposit, Employment Employment, bool HasOtherCredits = false);

public enum CreditGoal
{
    ConsumerCredit,
    RealEstate,
    OnLending
}

public enum Deposit
{
    RealEstate,
    OldCar,
    NewCar,
    Guarantor,
    None
}

public enum Employment
{
    Employee,
    SelfEmployed,
    Freelancer,
    Retired,
    Unemployed
}