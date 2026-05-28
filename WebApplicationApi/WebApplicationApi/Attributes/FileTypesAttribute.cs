using System.ComponentModel.DataAnnotations;

namespace WebApplicationApi.Attributes
{
    public class FileTypesAttribute : ValidationAttribute
    {
        private string[] _allowedTypes;
        public FileTypesAttribute(params string[] allowedTypes)
        {
            _allowedTypes = allowedTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<IFormFile> files = new List<IFormFile>();
            if (value is List<IFormFile> fileList) files = fileList;
            if (value is IFormFile singleFile) files.Add(singleFile);

            foreach (var file in files)
            {
                if (!_allowedTypes.Contains(file.ContentType))
                {
                    return new ValidationResult($"File type {file.ContentType} is not allowed. Allowed types are: {string.Join(", ", _allowedTypes)}");
                }
            }

            return ValidationResult.Success;
        }

    }
}
