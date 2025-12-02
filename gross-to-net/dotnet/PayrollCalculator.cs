using GrossToNet.Models;

namespace GrossToNet;

/// <summary>
/// Payroll calculator that processes earnings, deductions, and taxes to compute net pay.
///
/// CALCULATION: Net pay = gross pay - pre-tax deductions - taxes - post-tax deductions
///
/// See README.md for detailed requirements and examples.
/// </summary>
public class PayrollCalculator
{
  public PayrollResult Calculate(IEnumerable<Earning> earnings, IEnumerable<Deduction> deductions, IEnumerable<Tax> taxes)
  {

    // TODO: Implement payroll calculation logic here
    throw new NotImplementedException("Payroll calculation not yet implemented. Please implement the Calculate method.");
  }
}