# Gross-to-net Pay Check Coding Challenge

Requirements:

- Calculate the net pay as follows: net pay = taxable earnings - pre-tax deductions - taxes - post-tax deductions + non-taxable earnings
- Earning types
  - Salary earnings are defined by a flat `amount`
  - Hourly earnings are defined by an amount of `hours` and a `rate`
- Earnings that are not taxable are not withheld by deductions or taxes
- Pre-tax deductions are based on gross taxable-pay
- Flat vs percentage deductions (percentages encoded as `value / 100`)
- Taxes are based on gross taxable-pay less pre-tax deductions
- Taxes with a cap should not be withheld above the cap
- Post-tax deductions are based on gross taxable-pay less pre-tax deductions and taxes
- Deduction and tax priority
  - When taxable earnings are less than deductions and taxes, withholding priority is as follows: pre-tax deductions before taxes before post-tax deductions.
  - For each deduction and tax, a higher `priority` value indicates this item should be withheld first.
  - If a deduction or tax is cannot be withheld to the full amount, the deficit is recorded.
- Output the following
  - Gross pay
  - Net pay to employee
  - Amounts withheld for deductions and taxes by code with deficits

Sample input object:

```json
{
  "earnings": [
    {
      "code": "overtime",
      "type": "hourly",
      "hours": 20,
      "rate": 15,
      "isTaxable": true
    },
    {
      "code": "regular",
      "type": "hourly",
      "hours": 20,
      "rate": 13,
      "isTaxable": false
    },
    {
      "code": "bonus",
      "type": "salary",
      "amount": 1500,
      "isTaxable": true
    }
  ],
  "deductions": [
    {
      "code": "401k",
      "type": "percentage",
      "priority": 1,  
      "value": 10,
      "isPreTax": false
    },
    {
      "code": "healthInsurance",
      "type": "flat",
      "priority": 2, 
      "value": 20,
      "isPreTax": true
    }
  ],
  "taxes": [
    {
      "code": "federalIncome",
      "type": "percentage",
      "priority": 1,
      "value": 10
    },
    {
      "code": "fica",
      "type": "cappedPercentage",
      "priority": 2,
      "value": 10,
      "cap": 50
    }
  ]
}
```

All test cases and solutions can be found in `test-cases.json`