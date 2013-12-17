Feature: GodMode
	In order to test new features
	As a Dev or QA guy
	I want to overwrite the condition per cookie

Background: 
	Given I have feature with a condition evaluator that is never fullfilled

Scenario Outline: Conditions are not fulfilled but GodMode is enabled
	Given I have a feature in state <state>
		And I have enabled the GodMode
	When evaluating the feature state
	Then the <feature is enabled>

Examples: 
| state          | feature is enabled |
| In Development | true               |
| Under Test     | true               |
| Released       | true               |
