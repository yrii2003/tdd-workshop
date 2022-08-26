using System.Threading;
using System.Threading.Tasks;
using TddWorkshop.Domain.InstantCredit;

namespace TddWorkshop.Domain.InstantCredit
{
    public class CriminalRecordChecker : ICriminalRecordChecker
    {
        public Task<bool> HasCriminalRecordAsync(PassportInfo record, CancellationToken cancellationToken)
        {
            // return Task.FromResult(record.Series == "1234" && record.Number == "123456");
            throw new NotImplementedException();
        }
    }
}