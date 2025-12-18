namespace GrossToNet.Models;

/// <summary>
/// Represents a deduction from income (e.g., 401k, health insurance).
/// Deductions can be percentage-based or flat amounts, and can be pre-tax or post-tax.
/// </summary>
public class Deduction
{
    /// <summary>
    /// Identifier for the deduction type (e.g., "401k", "healthInsurance")
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// The deduction calculation type: "percentage" or "flat"
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Processing priority; lower values are processed first.
    /// Determines order of deduction application.
    /// </summary>
    public required int Priority { get; set; }

    /// <summary>
    /// The deduction value: percentage (0-100) or flat amount
    /// </summary>
    public required decimal Value { get; set; }

    /// <summary>
    /// Whether this deduction is applied before taxes (pre-tax) or after (post-tax)
    /// </summary>
    public required bool IsPreTax { get; set; }

}
