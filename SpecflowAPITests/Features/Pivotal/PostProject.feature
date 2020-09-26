Feature: Create Project

Scenario: Project is created with name
	Given I use the "pivotal" service client
	When I send a POST request to "projects" with the following json body
		"""
		{
			"name": "Test automation CSharp"
		}
		"""
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Pivotal/PostProjectSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath       | expectedValue          |
		| name           | Test automation CSharp |
		| kind           | project                |
		| week_start_day | Monday                 |