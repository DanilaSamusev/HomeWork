using System;
using System.Collections.Generic;
using System.Text;

namespace SiteDownloader.UI
{
    public interface IConstraint
    {
        ConstraintType ConstraintType { get; }
        bool IsAcceptable(Uri uri);
    }
}
