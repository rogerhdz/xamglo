using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinPO.Extensions
{
    public static class ExceptionExtension
    {
        public static string MessagePlusInner(this Exception ex)
        {
            return $"Error: {ex.Message}." + (ex.InnerException != null ? $"- Inner Excetion: {ex.InnerException}" : string.Empty);
        }
    }
}
