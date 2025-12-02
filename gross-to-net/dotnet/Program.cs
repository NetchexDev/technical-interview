using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GrossToNet.Models;

namespace GrossToNet;

class Program
{
  private const string TestFile = "test-cases.json";
  static void Main(string[] args)
  {
    // Check for help argument
    if (args.Any(arg => arg == "-h" || arg == "--help" || arg == "-?"))
    {
      DisplayHelp();
      Environment.Exit(0);
    }

    Console.WriteLine("=".PadRight(70, '='));
    Console.WriteLine("GROSS-TO-NET PAYROLL CALCULATOR - TEST RUNNER");
    Console.WriteLine("=".PadRight(70, '='));
    Console.WriteLine();

    // Check for custom file argument
    string? customFileName = null;
    for (int i = 0; i < args.Length; i++)
    {
      if ((args[i] == "-f" || args[i] == "--file") && i + 1 < args.Length)
      {
        customFileName = args[i + 1];
        break;
      }
    }

    string testFile;
    bool useAdvancedTests = false;

    if (customFileName != null)
    {
      testFile = customFileName;
    }
    else
    {
      const string AdvancedTestCases = "advanced-" + TestFile;
      var advancedFilePath = Path.Combine(AppContext.BaseDirectory, AdvancedTestCases);
      useAdvancedTests = File.Exists(advancedFilePath);
      testFile = useAdvancedTests ? AdvancedTestCases : TestFile;
    }

    var filePath = Path.Combine(AppContext.BaseDirectory, testFile);

    if (useAdvancedTests)
    {
      Console.WriteLine("Running ADVANCED test cases (advanced test file detected).");
    }
    else
    {
      Console.WriteLine("Running STANDARD test cases.");
    }

    if (!File.Exists(filePath))
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"✗ Test cases file not found: {filePath}");
      Console.ResetColor();
      Environment.Exit(1);
    }

    Console.WriteLine($"Loading " + testFile + $" from: {filePath}");
    var testCases = JsonSerializer.Deserialize<IEnumerable<TestCase>>(File.ReadAllText(filePath), new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    });

    if (testCases == null || !testCases.Any())
    {
      throw new Exception("Failed to load payroll data from input.json");
    }

    Console.WriteLine($"Loaded {testCases.Count()} test cases.");

    PayrollCalculator calculator = new PayrollCalculator();

    foreach (var testCase in testCases)
    {
      try
      {
        testCase.ActualOutput = calculator.Calculate(testCase.Input.Earnings, testCase.Input.Deductions, testCase.Input.Taxes);
      }
      catch (NotImplementedException nex)
      {
        testCase.Error = nex;
        testCase.Passed = false;
        testCase.Message = "Payroll calculation not yet implemented.";
      }
      catch (Exception ex)
      {
        testCase.Error = ex;
        testCase.Passed = false;
        testCase.Message = $"Calculator threw an exception: {ex.Message}";
      }

      ValidateTestCase(testCase);
    }

    var failedTests = testCases.Where(tc => tc.Passed == false).ToList();
    if (failedTests.Any())
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"✗ {failedTests.Count} out of {testCases.Count()} tests failed:");
      Console.ResetColor();
      Environment.Exit(1);
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"✓ All {testCases.Count()} test cases passed!");
    Console.ResetColor();
    Environment.Exit(0);

  }

  private static void ValidateTestCase(TestCase testCase)
  {
    var actual = testCase.ActualOutput!;

    if (testCase.Passed == false)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"\t ✗ Test failed: {testCase.Description} --> {(testCase.Error != null ? $"{testCase.Error.GetType().Name} --> " : string.Empty)}{testCase.Message}");
      Console.ResetColor();
      return;
    }

    if (testCase.ExpectedOutput == null)
    {
      testCase.Passed = false;
      testCase.Message = "No expected output provided for test case.";
      return;
    }
    var expected = testCase.ExpectedOutput!;

    if (actual.GrossPay == expected.GrossPay
      && actual.GrossTaxableEarnings == expected.GrossTaxableEarnings
      && actual.NetPay == expected.NetPay
      && actual.Withholdings.Count == expected.Withholdings.Count)
    {
      testCase.Passed = true;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"✓ Test passed: {testCase.Description}");
      Console.ResetColor();
      return;
    }

    testCase.Passed = false;

    Console.ForegroundColor = ConsoleColor.Red;

    if (actual.GrossPay != expected.GrossPay)
    {
      Console.WriteLine($"\tGrossPay mismatch  Actual: {actual.GrossPay} Expected: {expected.GrossPay}");
    }
    if (actual.GrossTaxableEarnings != expected.GrossTaxableEarnings)
    {
      Console.WriteLine($"\tGrossTaxableEarnings mismatch  Actual: {actual.GrossTaxableEarnings} Expected: {expected.GrossTaxableEarnings}");
    }
    if (actual.NetPay != expected.NetPay)
    {
      Console.WriteLine($"\tNetPay mismatch  Actual: {actual.NetPay} Expected: {expected.NetPay}");
    }
    if (actual.Withholdings.Count != expected.Withholdings.Count)
    {
      Console.WriteLine($"\tWithholdings count mismatch  Actual: {actual.Withholdings.Count} Expected: {expected.Withholdings.Count}");
    }
    Console.ResetColor();
  }

  private static void DisplayHelp()
  {
    Console.WriteLine("GROSS-TO-NET PAYROLL CALCULATOR - TEST RUNNER");
    Console.WriteLine();
    Console.WriteLine("USAGE:");
    Console.WriteLine("  dotnet run [options]");
    Console.WriteLine();
    Console.WriteLine("OPTIONS:");
    Console.WriteLine("  -f, --file <filename>    Specify a custom test file to run");
    Console.WriteLine("  -h, --help, -?           Display this help information");
    Console.WriteLine();
    Console.WriteLine("BEHAVIOR:");
    Console.WriteLine("  - If -f/--file is specified, runs the specified test file");
    Console.WriteLine("  - Otherwise, checks for 'advanced-test-cases.json' and runs it if found");
    Console.WriteLine("  - If no advanced file exists, runs 'test-cases.json' by default");
    Console.WriteLine();
    Console.WriteLine("EXAMPLES:");
    Console.WriteLine("  dotnet run");
    Console.WriteLine("  dotnet run -- -f custom-tests.json");
    Console.WriteLine("  dotnet run -- --file advanced-test-cases.json");
  }
}

public record PayrollData(IEnumerable<Earning> Earnings, IEnumerable<Deduction> Deductions, IEnumerable<Tax> Taxes);

// Helper classes for test case deserialization
public class TestCase
{
  [JsonPropertyName("input")]
  public required PayrollData Input { get; set; }

  [JsonPropertyName("output")]
  public PayrollResult? ExpectedOutput { get; set; }

  public string? Description { get; set; }

  [JsonIgnore]
  public bool? Passed { get; set; }
  [JsonIgnore]
  public string? Message { get; set; }
  [JsonIgnore]
  public Exception? Error { get; set; }
  [JsonIgnore]
  public PayrollResult? ActualOutput { get; set; }
}

