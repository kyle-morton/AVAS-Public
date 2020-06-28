using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Text;

namespace AVAS.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetErrors(this ModelStateDictionary modelState)
        {
            var sb = new StringBuilder();
            foreach (var error in modelState.Values.SelectMany(v => v.Errors))
            {
                sb.Append(error.ErrorMessage + " ");
            }
            return sb.ToString();
        }
    }
}
