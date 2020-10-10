using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using TradeProcessor.Services.Contracts;

namespace TradeProcessor.Services.Implementations
{
    public class RegexService : IRegexService
    {
        private readonly IInputValidator _inputValidator;
        private Regex _regex;

        public RegexService(IConfiguration configuration, IInputValidator inputValidator)
        {
            _inputValidator = inputValidator;

            var pattern = configuration["RegexPattern"];

            _regex = new Regex(pattern);
        }

        public List<string> Execute(string input)
        {
            var valid = _inputValidator.Validate(input);

            if (!valid)
            {
                throw new ArgumentException($"Invalid input: {input}");
            }

            var match = _regex.Match(input);

            return match.Groups.Skip(1).Select(g => g.Value).ToList();
        }
    }
}
