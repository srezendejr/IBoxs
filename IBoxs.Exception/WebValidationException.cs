using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;

namespace IBoxs.Excecao
{
    public class WebValidationException : BaseException
    {
        readonly ModelStateDictionary _modelState;

        public WebValidationException(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public IEnumerable<ModelError> GetErrors()
        {
            return _modelState.Values.SelectMany(m => m.Errors);
        }

        public override List<string> GetAllInnerMessages()
        {
            return _modelState.Values.SelectMany(m => m.Errors).Select(item => item.ErrorMessage).ToList();
        }
    }
}