using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddWorkshop.Domain.InstantCredit
{
    public class CreditInfoValidator : AbstractValidator<CreditInfo>
    {
        public CreditInfoValidator()
        {
            RuleFor(x => x.Deposit).IsInEnum();
        }
    }


    public class PassportInfoValidator : AbstractValidator<PassportInfo>
    {
        public PassportInfoValidator()
        {
            RuleFor(x => x.Series).Length(4, 4);
            RuleFor(x => x.Number).Length(6, 6);
            RuleFor(x => x.IssueDate).LessThan(DateTime.Today);
        }
    }
}
