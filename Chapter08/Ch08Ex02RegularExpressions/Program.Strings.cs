using System.Diagnostics.CodeAnalysis;

partial class Program
{
    [StringSyntax(StringSyntaxAttribute.Regex)]
    public const string ContainsDigitText = @"^\d+$";
}
