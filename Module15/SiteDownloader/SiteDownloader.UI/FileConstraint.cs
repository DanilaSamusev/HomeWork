using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteDownloader.UI
{
    class FileConstraint : IConstraint
    {
        private readonly IEnumerable<string> _acceptableExtensions;

        public FileConstraint(IEnumerable<string> acceptableExtensions)
        {
            _acceptableExtensions = acceptableExtensions;
        }

        public ConstraintType ConstraintType => ConstraintType.FileConstraint;

        public bool IsAcceptable(Uri uri)
        {
            string lastSegment = uri.Segments.Last();
            return _acceptableExtensions.Any(e => lastSegment.EndsWith(e));
        }
    }
}
