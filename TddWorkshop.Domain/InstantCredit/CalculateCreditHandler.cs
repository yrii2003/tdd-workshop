using MediatR;

namespace TddWorkshop.Domain.InstantCredit;

public class CalculateCreditHandler : IRequestHandler<CalculateCreditRequest, CalculateCreditRespons>
{
    private readonly ICriminalRecordChecker _criminalRecordChecker;

    public CalculateCreditHandler(ICriminalRecordChecker criminalRecordChecker)
    { 
        _criminalRecordChecker = criminalRecordChecker;
    }

    public async Task<CalculateCreditRespons> Handle(CalculateCreditRequest request, CancellationToken cancellationToken)
    {
        var cr = await _criminalRecordChecker.HasCriminalRecordAsync(request.PassportInfo, cancellationToken);
        return CreditCalculator.Calculate(request, cr);

    }
}