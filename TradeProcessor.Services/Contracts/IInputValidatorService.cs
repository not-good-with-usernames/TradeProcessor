namespace TradeProcessor.Services.Contracts
{
    public interface IInputValidatorService
    {
        bool Validate(string input);
    }
}