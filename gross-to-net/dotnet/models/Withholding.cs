namespace GrossToNet.Models;

/// <summary>
/// Represents a single withholding entry (tax or deduction) that was applied
/// </summary>
public class Withholding
{
    /// <summary>
    /// Type of withholding: "deduction" or "tax"
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Code identifying the specific deduction or tax
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Amount actually withheld (may be less than requested if insufficient funds)
    /// </summary>
    public decimal AmountWithheld { get; set; }

    /// <summary>
    /// Amount that could not be withheld due to insufficient remaining pay
    /// </summary>
    public decimal Deficit { get; set; }
}
