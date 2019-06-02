namespace CoolBrains.Bus.Contracts.Command
{
    public class CommandResponse
    {
        public bool Success { get; set; }
        public dynamic Result { get; set; }
        public object ValidationResult { get; set; } //TODO it should be specific type
        public string ErrorMessage { get; set; }
    }
}
