namespace GrossToNet.Models;

/// <summary>
/// Represents an earning item that contributes to gross income.
/// Can be hourly-based (calculated from hours and rate) or salary-based (flat amount).
/// </summary>
public class Earning
{
    /// <summary>
    /// Identifier for the earning type (e.g., "overtime", "regular", "bonus")
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// The earning calculation type: "hourly" or "salary"
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Number of hours worked (only used when Type is "hourly")
    /// </summary>
    public decimal? Hours { get; set; }

    /// <summary>
    /// Hourly rate (only used when Type is "hourly")
    /// </summary>
    public decimal? Rate { get; set; }

    /// <summary>
    /// Fixed salary amount (only used when Type is "salary")
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// Whether this earning is subject to taxation
    /// </summary>
    public required bool IsTaxable { get; set; }
}
