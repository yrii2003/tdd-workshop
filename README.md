# How to use this repository
- Create a fork of the repository
- Start with the "master" branch
- Read the next two paragraphs (*Requirements* and *Step 1*) of this README.md file
- Create a new branch from the "master" branch; you can call it "my-step-1"
- Complete the first task, commit and push your implementation to "my-step-1"
- Check the "step-1" branch and compare it to "my-step-1"
- Create a new branch from the "my-step-1" branch; you can call it "my-step-2"
- Repeat the process until you reach the end of this README.md file

# Requirements
The bank needs an instant credit calculator. A bank employee must be able to fill out the form and get an instant resolution
whether the credit application is satisfied or not. The interest rate must be calculated automatically based on the from data according 
to the rules below:

| Criterion       | Condition              | Points                                                                                                                              |
|-----------------|------------------------|-------------------------------------------------------------------------------------------------------------------------------------|
| Age             | 21-28                  | When the credit sum: <br/>- Is less than 1.000.000 - 12 <br/>- From 1.000.000 to 3.000.000 - 9<br/> - Is greater than 3.000.000 - 0 |
|                 | 29-59                  | 14                                                                                                                                  |
|                 | 60-72                  | - With deposit - 8<br/> - Without deposit - 0                                                                                       |
| Criminal record | Yes                    | 0                                                                                                                                   |
|                 | No                     | 15                                                                                                                                  |
| Income          | Employee               | 14                                                                                                                                  |
|                 | Self-employed          | 14                                                                                                                                  |
|                 | Freelancer             | 8                                                                                                                                   |
|                 | Retired                | When the age is:<br/> - Less than 70 - 5<br/> - Otherwise - 0                                                                       |
|                 | Uneployed              | 0                                                                                                                                   |
| Credit goal     | Сonsumer               | 14                                                                                                                                  |
|                 | Real estate / mortgage | 8                                                                                                                                   |
|                 | On-lending             | 12                                                                                                                                  |
| Deposit         | Real estate            | 14                                                                                                                                  |
|                 | Car                    | - No older than three years - 8                                                                                                     |
|                 |                        | - Older than three years - 3                                                                                                        |
|                 | Guarantor              | 12                                                                                                                                  |
| Other credits   | No                     | Credit goal = On-lending:<br/> - Yes - 0<br/> - No - 15                                                                             |
|                 | Yes                    | 0                                                                                                                                   |                                                                                                                                   
| Sum             | 0 - 1.000.000          | 12                                                                                                                                  |
|                 | 1.000.001 - 5.000.000  | 14                                                                                                                                  |
|                 | 5.000.001 - 10.000.000 | 8                                                                                                                                   |

Criminal record information will be available via calling 3rd party API which is not ready yet.  

## Interest rate based on points
| Points | Interest rate |
|--------|---------------|
| < 80   | N/A           |
| 80     | 30%           |
| 84     | 26%           |
| 88     | 22%           |
| 92     | 19%           |
| 96     | 15%           |
| 100    | 12,5%         |

# Steps
![](https://miro.medium.com/max/1400/0*VjbieOROPmnqlGCw.png)
## Step 1
- Implement ```CreditCalculator``` via ```CreditCalculatorTests```
- Use ```Bogus``` to generate test data
- Use 3 datasets with xUnit (control examples)
- Check coverage
- Don't implement ```CalculateAsync```, implement ```Calculate``` instead
- Introduce ```ICriminalRecordChecker``` interface

## Step 2
- Use ```AutoFixture``` to generate test data
- Use ```FsCheck``` to improve coverage

## Step 3
- Implement a request handler using ```MediatR```
- Implement validators, using ```FluentValidator```

## Step 4
- Use ```WebApplicationFactory``` to test validator
- Use ```Moq``` to mock ```CriminalRecordChecker```

## Step 5
- Implement View
- Test View using ```WebDriver```

## Step 6
- Replace View with React
- Update e2e tests

## Honorable mentions
- Pex/VSharp - symbolic execution
- K6 - stress testing  
- Stryker.NET - mutation testing