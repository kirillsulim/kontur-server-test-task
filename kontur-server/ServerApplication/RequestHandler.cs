using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kontur_server_core.Autocompleter;
using System.Text.RegularExpressions;

namespace kontur_server.ServerApplication
{
    public class RequestHandler : IRequestHandler
    {
        private IAutocompleter autocompleter;

        public RequestHandler(IAutocompleter autocompleter)
        {
            if(autocompleter == null)
                throw new ArgumentNullException();

            this.autocompleter = autocompleter;
        }

        public string[] HandleRequestToAutocompleter(string request)
        {
            request = request.ToLower();
            Regex pattern = new Regex("get [a-z]+");
            if (!pattern.IsMatch(request))
            {
                throw new ProcessingException();
            }

            return autocompleter.Get(request.Substring(4));
        }
    }
}
