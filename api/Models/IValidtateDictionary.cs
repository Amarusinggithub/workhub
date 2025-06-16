namespace api.Models;

public interface IValidtateDictionary
{
    void AddError(string key, string errorMessage);
    bool IsValid { get; }
}
