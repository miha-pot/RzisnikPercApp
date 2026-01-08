using System.ComponentModel.DataAnnotations;

namespace RPApplication.Services.Helpers
{
    public class ValidationHelper
    {
        //In service validation
        public static void ModelValidation(object obj)
        {
            ValidationContext validationContext = new(obj);
            List<ValidationResult> validationResults = [];
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }

        //In endpoint validation
        public static bool IsModelValid(object obj, out List<ValidationResult> errors)
        {
            errors = [];

            var validationContext = new ValidationContext(obj);
            return Validator.TryValidateObject(obj, validationContext, errors, true);
        }
    }
}
