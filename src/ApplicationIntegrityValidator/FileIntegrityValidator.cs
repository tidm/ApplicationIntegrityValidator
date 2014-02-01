using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplicationIntegrityValidator
{
    public class FileIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {
        private readonly string _fileName;
        private readonly List<IntegrityValidationResult> _results;

        public FileIntegrityValidator Exists()
        {
            var result = new IntegrityValidationResult
            {
                Succeed = File.Exists(_fileName),
                Description = String.Format("Ensure File {0} exists in {1}", Path.GetFileName(_fileName), Path.GetDirectoryName(_fileName)),
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public FileIntegrityValidator HasAttributes(FileAttributes fileAttributes)
        {
            var attrs = File.GetAttributes(_fileName);
            var result = new IntegrityValidationResult()
            {
                Succeed = (attrs & fileAttributes) == fileAttributes,
                Description = string.Format("Ensure File {0} has {1} attributes", Path.GetFileName(_fileName), fileAttributes),
                Exception = null
            };
            _results.Add(result);
            return this;
        }

        public FileIntegrityValidator(string fileName)
        {
            _fileName = fileName;
            _results = new List<IntegrityValidationResult>();
        }

        public IEnumerator<IntegrityValidationResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }
    }
}
