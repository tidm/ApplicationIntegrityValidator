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
    }
}
