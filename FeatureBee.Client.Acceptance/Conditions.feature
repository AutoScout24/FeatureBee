Feature: Conditions
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background:
	Given I have feature under test with a condition evaluator

Scenario Outline: The Feature has one condition
	Given the condition is <condition>
	When evaluating the feature state
	Then the <feature is enabled>

Examples: 
| condition | feature is enabled |
| false     | false				 |
| true      | true				 |

Scenario Outline: The feature has multiple condition evaluator
	Given the feature has a second condition evaluator
	And the first condition evaluator returns <first evaluator>
	And the second condition evaluator returns <second evaluator>
	When evaluating the feature state
	Then the <feature is enabled>

Examples: 
| first evaluator | second evaluator | feature is enabled |
| false           | false            | false			  |
| true            | false            | false			  |
| false           | true             | false			  |
| true            | true             | true 			  |

