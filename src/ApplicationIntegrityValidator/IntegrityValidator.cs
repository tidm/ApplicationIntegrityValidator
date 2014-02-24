using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationIntegrityValidator
{
    public class IntegrityValidator
    {
        public FileIntegrityValidator File(string fileName)
        {
            return new FileIntegrityValidator(fileName);
        }

        public FolderIntegrityValidator Folder(string folderName)
        {
            return new FolderIntegrityValidator(folderName);
        }

        public AppConfigIntegrityValidator AppConfig()
        {
            return new AppConfigIntegrityValidator();
        }

        public DatabaseIntegrityValidator Database(string connectionString)
        {
            return new DatabaseIntegrityValidator(connectionString);
        }

        public WebServiceIntegrityValidator WebService(string uri)
        {
            return new WebServiceIntegrityValidator(uri);
        }

    }
}
