using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplicationIntegrityValidator
{
    public class FolderIntegrityValidator : IEnumerable<IntegrityValidationResult>
    {
        private readonly string _folderName;
        private readonly List<IntegrityValidationResult> _results;

        public FolderIntegrityValidator(string folderName)
        {
            _folderName = folderName;
            _results = new List<IntegrityValidationResult>();
        }

        public FolderIntegrityValidator Exists()
        {
            var result = new IntegrityValidationResult()
                         {
                             Description = String.Format("Ensure Folder {0} exists", Path.GetDirectoryName(_folderName)),
                             Exception = null,
                             Succeed = Directory.Exists(_folderName)
                         };
            _results.Add(result);
            return this;
        }

        public FolderIntegrityValidator HasAttributes(FileAttributes fileAttributes)
        {
            var folder = new DirectoryInfo(_folderName);
            var result = new IntegrityValidationResult()
                         {
                             Exception = null,
                             Description = string.Format("Ensure Folder {0} has {1} attribute(s)", Path.GetDirectoryName(_folderName), fileAttributes),
                             Succeed = (folder.Attributes & fileAttributes) == fileAttributes
                         };
            _results.Add(result);
            return this;
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
