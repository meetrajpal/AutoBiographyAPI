using System.ComponentModel.DataAnnotations;
namespace AutoBiographyAPI.Attributes;

public class MustBeTrueAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is bool boolValue)
        {
            return boolValue;
        }
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return name;
    }
}
