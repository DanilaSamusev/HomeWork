using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SiteDownloader.UI
{
    public interface IContentSaver
    {
        void SaveFile(Uri uri, Stream fileStream);
        void SaveHtmlDocument(Uri uri, string name, Stream documentStream);
    }
}
