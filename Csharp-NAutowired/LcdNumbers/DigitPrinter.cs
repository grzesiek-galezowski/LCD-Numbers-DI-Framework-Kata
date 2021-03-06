using System.Collections.Generic;
using System.Linq;
using NAutowired.Core.Attributes;

namespace Org.Codecop.Lcdnumbers
{
    /// <summary>Appends lines next to each other and adds line breaks.</summary>
    [Service]
    public class DigitPrinter : IDigitPrinter
    {
        private const string Newline = "\n";
        [Autowired]
        private readonly IZipper zipper;

        public virtual string Render(IList<Digit> digits)
        {
            IList<IList<Line>> linesOfAllDigits = LinesOfAllDigits(digits);
            IList<string> linesSideBySide = Zip(linesOfAllDigits);
            return Join(linesSideBySide);
        }

        private IList<IList<Line>> LinesOfAllDigits(IList<Digit> digits)
        {
            return digits.Select(d => d.Lines()).ToList();
        }

        private IList<string> Zip(IList<IList<Line>> linesOfAllDigits)
        {
            return zipper.Zip(linesOfAllDigits, (e) => Concat(e));
        }

        private string Concat(IList<Line> lines)
        {
            return lines //
                .Select(l => l.ToString()) //
                .Aggregate((current, next) => current + next);
        }

        private string Join(IList<string> lines)
        {
            return lines //
                .Select(l => l.ToString()) //
                .Aggregate((current, next) => current + Newline + next) + Newline;
        }
    }
}
