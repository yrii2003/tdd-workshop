namespace TddWorkshop.Domain.InstantCredit
{
    public interface ICriminalRecordChecker
    {
        Task<bool> HasCriminalRecordAsync(PassportInfo PassportInfo, CancellationToken cancellationToken);
    }
}