Feature: FeatureStates
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers
	
Background: 
	Given I have feature with a condition evaluator that is always fullfilled

Scenario Outline: Based on the State of the feature
	Given I have a feature in state <state>
	When evaluating the feature state
	Then the <feature is enabled>
	And <conditions were used> to evaluate the state

Examples:
| state			 | feature is enabled | conditions were used |
| In Development | false			  | false                |
| Under Test	 | true				  | true                 |
| Released       | true				  | false                |
