# Gross-to-Net Payroll Calculator

Implement the `Calculate` method in `PayrollCalculator.cs` which accepts three collections as arguments... `earnings`, `deductions`, and `taxes` then returns a `PayrollResult` with `grossPay`, `grossTaxableEarnings`, a list of `witholdings`, and `netPay`.

## Requirements

### NetPay Calculation

`NetPay = GrossTaxableEarnings - PreTaxDeductions - Taxes - PostTaxDeductions + NonTaxableEarnings`

### Earnings

- Types: `salary`, `hourly`
- Salary earnings have a flat `amount`
- Hourly earnings amount is `hours × rate`
- Only taxable earnings are included when calculating deductions/taxes

### Deductions

- Types: `flat`, `percentage`
- Flat decductions have a fixed dollar `value`
- Percentage deductions are calculated as `value / 100` of basis
- Pre-tax deductions reduce tax basis, calculated with gross taxable earnings
- Post-tax deductions do not reduce tax basis

### Taxes

- Types: `percentage`, `cappedPercentage`
- Percentage taxes are calculated as `value / 100` of basis using (gross taxable earnings - pre-tax deductions)
- Capped Percentage has a max value defined by the `cap` property

### Witholdings

- Types: `tax`, `deductions`
- Deductions are either Pre-Tax or Post-Tax depending on the `isPrefix` value.
- Witholding groups are applied using the following order:
  - PreTax Deductions
  - Taxes
  - PostTax Deductions
- Within each group, withholdings are applied in order of their `priority` value (highest `priority` first)
- When a withholding exceeds available earnings the difference is added as a `deficit` as NetPay cannot go below zero.

## Build and Run

To test your solution, Program.main() contains a test harness for loading test data from a json file and comparing the expected output to the actual output provided by your `Calculate` method and report the result.

A json file containing basic test cases is included in the project, `test-cases.json`, but you are encouraged to add additional test cases as needed. Additional test cases may be used to verify edge cases and common defects during your interview.

### IDE

An online VSCode codespace will be used during assesmments and candidates are free to install extensions available in the vs code default extension marketplace.

Start Debug: `F5`

### Commmand-line

Navigate to the project root e.g. `gross-to-net/dotnet/` then use _dotnet_ CLI commands.

Building from CLI

```ps
dotnet build
```

Running from CLI

```ps
dotnet run
```

Show Help Text

```ps
dotnet run -- --help
```