using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationIntegrityValidator
{
    public class IntegrityValidationResult
    {
        public string Description { get; set; }
        public Exception Exception { get; set; }
        public bool Succeed { get; set; }
    }
}
