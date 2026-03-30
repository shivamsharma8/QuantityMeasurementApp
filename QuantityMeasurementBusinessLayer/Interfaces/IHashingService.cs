namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        string ComputeSha256(string input);
    }
}
