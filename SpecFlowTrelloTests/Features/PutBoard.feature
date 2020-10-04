Feature: Update project

Background: Create project
	Given  I use the "Trello" service client
	When I send a POST request to "boards" with the following body
		"""
		{
			"name":"Board test01",
		}
		"""
	And I store board id for workstapce cleaning
	And I store response "id" value as "PROJECT ID"
	Then I validate that status code returned is "200"

@deleteTrelloBoard
Scenario: Board is updated with name
	Given  I use the "Trello" service client
	When I send a PUT request to "boards/(PROJECT_ID)" with the following body
		"""
		{
			"name":"Board name modified",
		}
		"""
	Then I validate that status code returned is "200"
	And I validate that the response body match "Schemas/PutSchema.json" JSON schema
	And I validate that response body contains the following values
		| jsonpath | expectedValue                   |
		| name     | Board name modified             |


