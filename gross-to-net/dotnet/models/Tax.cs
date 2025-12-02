namespace GrossToNet.Models;

/// <summary>
/// Represents a tax calculation applied to income.
/// Can be a simple percentage or a capped percentage (with a maximum tax amount).
/// </summary>
public class Tax
{
    /// <summary>
    /// Identifier for the tax type (e.g., "federalIncome", "fica")
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// The tax calculation type: "percentage" or "cappedPercentage"
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Processing priority; lower values are processed first.
    /// Determines order of tax calculation.
    /// </summary>
    public required int Priority { get; set; }

    /// <summary>
    /// The tax rate as a percentage (0-100)
    /// </summary>
    public required decimal Value { get; set; }

    /// <summary>
    /// Maximum tax amount (only used when Type is "cappedPercentage")
    /// </summary>
    public decimal? Cap { get; set; }
}
