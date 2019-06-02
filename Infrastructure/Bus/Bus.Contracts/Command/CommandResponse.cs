using SuitSupply.Infrastructure.Validator.Contract;

namespace SuitSupply.Infrastructure.Bus.Contracts.Command
{
    public class CommandResponse
    {
        public bool Success { get; set; }
        public dynamic Result { get; set; }
        public SuitValidationResult ValidationResult { get; set; }
        public string ErrorMessage { get; set; }
    }
}
