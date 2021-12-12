using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model.In.Base
{
    public abstract class BaseIn
    {
        //valida a model pelo Data Annotation
        public virtual bool ValidationModel()
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);
            Validator.TryValidateObject(this, validationContext, result);
            if (this is IValidatableObject) (this as IValidatableObject).Validate(validationContext);
            ArgumentException ex = null;
            result.ForEach((item) =>
            {
                ex = new ArgumentException(item.ErrorMessage, ex);
            });
            if (result.Any())
                throw new ArgumentException(ValidationText.Error, ex);

            return true;
        }
    }
}
