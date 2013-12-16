Feature: Conditions
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given I have feature under test with a condition evaluator

Scenario: Condition is true
	Given the condition is <condition>
	When evaluating the feature state
	Then the feature is <enabled>

Scenario: The feature has multiple condition evaluator
	Given the feature has a second condition evaluator
	And the first condition evaluator returns <first evaluator>
	And the second condition evaluator returns <second evaluator>
	When evaluating the feature state
	Then the feature is <enabled>

| first evaluator | second evaluator | enabled |
| false           | false            | false   |
| true            | false            | false   |
| false           | true             | false   |
| true            | true             | true    |

Scenario: The condition has multiple values
	Given the evaluator has two conditions
	And the first condition is <first condition>
	And the second condition is <second condition>
	When evaluating the feature state
	Then the feature is <enabled>

| first condition | second condition | enabled |
| false           | false            | false   |
| true            | false            | true    |
| false           | true             | true    |
| true            | true             | true    |

	 
