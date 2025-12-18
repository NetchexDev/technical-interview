namespace GrossToNet.Models;

/// <summary>
/// Represents the result of a payroll calculation
/// </summary>
public class PayrollResult
{
    /// <summary>
    /// Total gross pay (sum of all earnings)
    /// </summary>
    public decimal GrossPay { get; set; }

    /// <summary>
    /// Total gross taxable earnings (sum of earnings marked as taxable)
    /// </summary>
    public decimal GrossTaxableEarnings { get; set; }

    /// <summary>
    /// List of all withholdings (deductions and taxes) applied in order
    /// </summary>
    public List<Withholding> Withholdings { get; set; } = new();

    /// <summary>
    /// Net pay after all withholdings
    /// </summary>
    public decimal NetPay { get; set; }
}
