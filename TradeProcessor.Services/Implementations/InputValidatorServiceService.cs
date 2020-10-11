using Microsoft.Extensions.Logging;
using TradeProcessor.Services.Contracts;

namespace TradeProcessor.Services.Implementations
{


    public class InputValidatorServiceService : IInputValidatorService
    {
        private readonly ILogger<InputValidatorServiceService> _logger;

        public InputValidatorServiceService(ILogger<InputValidatorServiceService> logger)
        {
            _logger = logger;
        }

        public bool Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                _logger.LogWarning($"Line contains no data.");

                return false;
            }

            var fields = input.Split(new[] { ',' });
            if (fields.Length != 3)
            {
                _logger.LogWarning($"Line \"{input}\" malformed. Only {fields.Length} fields found.");

                return false;
            }

            if (fields[0].Length != 6)
            {
                _logger.LogWarning($"Trade currencies for line \"{input}\" malformed. {fields[0]}");

                return false;
            }

            if (!int.TryParse(fields[1], out _))
            {
                _logger.LogWarning($"Trade amount for line \"{input}\" is not a valid integer. {fields[1]}");
                return false;
            }

            if (!decimal.TryParse(fields[2], out _))
            {
                _logger.LogWarning($"Trade price for line \"{input}\" is not a valid decimal. {fields[2]}");
                return false;
            }

            return true;
        }
    }
}