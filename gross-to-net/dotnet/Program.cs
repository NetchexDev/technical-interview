namespace GrossToNet;

class Program
{
    private static string InputFilePath => Path.Combine(AppContext.BaseDirectory, "input.json");

    static void Main(string[] args)
    {

      PaycheckInput input = new();
      List<Withholding> withholdings = new();
      // Your code goes here!

      // We calculate the earnings
      CalculatedEarnings calculatedEarnings = CalculatedEarnings(input);

      // We process pre-tax deductions
      ProcessTaxDdeductions(input, calculatedEarnings, withholdings, true);

      // We process taxes
      ProcessTaxes(input, calculatedEarnings, withholdings);

      // We process post-tax deductions
      ProcessTaxDdeductions(input, calculatedEarnings, withholdings, false);

      PaycheckOutput output = new PaycheckOutput
      {
        GrossPay = calculatedEarnings.TotalEarnings,
        GrossTaxableEarnings = calculatedEarnings.TaxableEarnings,
        Withholdings = withholdings,
        NetPay = calculatedEarnings.RemainingTaxableEearnings + calculatedEarnings.NonTaxableEarnings
      };
     
    }

    public static void ProcessTaxes(PaycheckInput input, CalculatedEarnings calculatedEarnings, List<Withholding> withholdings)
    {
      var taxes = input.Taxes
          .OrderBy(t => t.Priority)
          .ToList();

      foreach(var tax in taxes)
      {
        decimal amountToTax = (calculatedEarnings.RemainingTaxableEearnings * tax.Value / 100);

        decimal 
      }
    }

    public static CalculatedEarnings CalculatedEarnings(PaycheckInput input)
    {
      // Your code goes here!
      decimal TaxableEarnings = 0;
      decimal NonTaxableEarnings = 0;

      foreach(var earning in input.Earnings)
      {
        decimal amount = earning.Type == "hourly" ? (earning.Hours ?? 0) * (earning.Rate ?? 0) : (earning.Amount ?? 0);

       if(earning.IsTaxable)
        {
          TaxableEarnings += amount;
        }
        else
        {
          NonTaxableEarnings += amount;
        }
      }

      return new CalculatedEarnings
      {
        TotalEarnings = TaxableEarnings + NonTaxableEarnings,
        TaxableEarnings = TaxableEarnings,
        NonTaxableEarnings = NonTaxableEarnings,
        RemainingTaxableEearnings = TaxableEarnings
      };
    }

    public static void ProcessTaxDdeductions(PaycheckInput input, CalculatedEarnings calculatedEarnings, List<Withholding> withholdings, bool isPreTax)
    {
        var deductions = input.Deductions.Where(d => d.IsPreTax == isPreTax)
            .OrderBy(d => d.Priority)
            .ToList();

        foreach(var deduction in deductions)
        {
            decimal amoutToDeduct = deduction.Type == "fixed" ? deduction.Value : (calculatedEarnings.RemainingTaxableEearnings * deduction.Value / 100);

            decimal 
        }
    }


}



public class CalculatedEarnings
{
    public decimal TotalEarnings { get; set; }
    public decimal TaxableEarnings { get; set; }
    public decimal NonTaxableEarnings { get; set; }
    public decimal RemainingTaxableEearnings { get; set; }
}

public class PaycheckInput
{
    public List<Earning> Earnings { get; set; } = new();
    public List<Deduction> Deductions { get; set; } = new();
    public List<Tax> Taxes { get; set; } = new();
}

public class Earning
{
    public string Code { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal? Hours { get; set; }
    public decimal? Rate { get; set; }
    public decimal? Amount { get; set; }
    public bool IsTaxable { get; set; }
}



public class Deduction
{
    public string Code { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Priority { get; set; }
    public decimal Value { get; set; }
    public bool IsPreTax { get; set; }
}

public class Tax
{
    public string Code { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Priority { get; set; }
    public decimal Value { get; set; }
    public decimal? Cap { get; set; }
}

public class PaycheckOutput
{
    public decimal GrossPay { get; set; }
    public decimal GrossTaxableEarnings { get; set; }
    public List<Withholding> Withholdings { get; set; } = new();
    public decimal NetPay { get; set; }
}

public class Withholding
{
    public string Type { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal AmountWithheld { get; set; }
    public decimal Deficit { get; set; }
}