# Visio Financial Services - Dynamic Product Pricing

This repository contains the C# console application that allows Visio Financial Services to dynamically generate product pricing based on a set of rules defined by the finance team.

## Project Description

The system calculates product interest rates based on a combination of person-specific information, product details, and the finance team's pricing rules. These components are provided as JSON files within the project:

* Person: Represents individual customers and their attributes, such as location and credit score.
* Product: Represents various financial products with their base interest rates.
* Rules: Define how to adjust product pricing based on the person's attributes.

The final interest rate of a product varies for each person due to their unique attributes and the dynamic application of the rules.

### Initial Rules:

1. All products start at 5.0 interest_rate.
2. If the person lives in Florida, the product is disqualified.
3. If the person has a credit score greater than or equal to 720, then the interest_rate on the product is reduced by .3.
4. If the person has a credit score lower than 720, the interest_rate on the product is increased by .5.
5. If the name of the product is "7-1 ARM", then 0.5 is added to the interest_rate of the product.

### Additional Rules:
1. If the name of the product contains "Cabin", then reduce 0.5 interest_rate of the product.
2. Winterfell has a default interest rate of 4.0.

## Getting Started

### Prerequisites

- .NET 6.0 SDK

### Running the Project

1. Navigate to the root directory of the project.
2. Run the following command to execute the project:

```bash
dotnet run --project RulesEng.csproj
```
![image](https://github.com/ZeyuanLi16/RulesEng/assets/22227133/65031d72-a981-476a-8195-7f50cb0d461f)

### Running Tests

1. Navigate to the test project directory.
2. Run the following command to execute the tests:

```bash
dotnet test RulesEng.Test.csproj
```

## Feedback and Contributions

For feedback, issues, or contributions, please submit a pull request or raise an issue on this repository.
