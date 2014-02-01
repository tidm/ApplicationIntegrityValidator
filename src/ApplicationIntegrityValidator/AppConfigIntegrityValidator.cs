using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ApplicationIntegrityValidator
{
    public class AppConfigIntegrityValidator
    {
        public AppSettingsIntegrityValidator AppSettings(string key)
        {
            return new AppSettingsIntegrityValidator(key);
        }

        public ConnectionStringsIntegrityValidator ConnectionStrings(string name)
        {
            return new ConnectionStringsIntegrityValidator(name);
        }
    }
}
