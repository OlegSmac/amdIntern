using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project1.Lesson_13_Assignment.BusinessLayer
{
    public class WebBrowser
    {
        private static readonly Dictionary<string, BrowserName> _browserNames = new()
        {
            { "IE", BrowserName.InternetExplorer },
            { "Firefox", BrowserName.Firefox },
            { "Chrome", BrowserName.Chrome },
            { "Opera", BrowserName.Opera },
            { "Safari", BrowserName.Safari },
            { "Dolphin", BrowserName.Dolphin },
            { "Konqueror", BrowserName.Konqueror },
            { "Linx", BrowserName.Linx }
        };
        
        public BrowserName Name { get; set; }
        public int MajorVersion { get; set; }

        public WebBrowser(string name, int majorVersion)
        {
            Name = TranslateStringToBrowserName(name);
            MajorVersion = majorVersion;
        }

        private BrowserName TranslateStringToBrowserName(string name)
        {
            if (_browserNames.TryGetValue(name, out BrowserName browserName)) return browserName;
                
            return BrowserName.Unknown;
        }

        public enum BrowserName
        {
            Unknown,
            InternetExplorer,
            Firefox,
            Chrome,
            Opera,
            Safari,
            Dolphin,
            Konqueror,
            Linx
        }
    }
}