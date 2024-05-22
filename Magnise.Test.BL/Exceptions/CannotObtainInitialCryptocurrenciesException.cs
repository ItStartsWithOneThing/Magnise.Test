
namespace Magnise.Test.BL.Exceptions
{
    public class CannotObtainInitialCryptocurrenciesException : Exception
    {
        public CannotObtainInitialCryptocurrenciesException(string msg) : base($"Cannot obtain initial currencies. {msg}") {}
    }
}
