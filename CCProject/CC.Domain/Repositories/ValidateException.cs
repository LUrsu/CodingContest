using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace CC.Domain.Repositories
{
    public class ValidateException : Exception
    {
        public ValidateException(IEnumerable<DbEntityValidationResult> validationResults)
        {
            ValidationErrors = validationResults;
        }

        public IEnumerable<DbEntityValidationResult> ValidationErrors { get; set; }
    }
}
