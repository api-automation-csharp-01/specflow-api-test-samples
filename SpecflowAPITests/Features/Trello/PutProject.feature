@deleteTrelloBoard
Feature: Update Board

Background: Create board
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test automation CSharp"
		}
		"""
	And I store board id for workspace cleaning
	And I store response "id" value as "PROJECT_ID"
	Then I validate that the response status code is "200"

Scenario: Board is updated on name, description and permissionLevel
	Given I use the "Trello" service client
	When I send a "Trello" PUT request to "boards/{PROJECT_ID}" with the following json body
		"""
		{
			"name": "New project name",
			"desc": "First Board for Automation API task UPDATED",
			"prefs/permissionLevel": "private"
		}
		"""
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PutBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath              | expectedValue                               |
		| name                  | New project name                            |
		| desc                  | First Board for Automation API task UPDATED |
		| prefs.permissionLevel | private                                     |

Scenario: Board is not updated with empty name
	Given I use the "Trello" service client
	When I send a "Trello" PUT request to "boards/{PROJECT_ID}" with the following json body
		"""
		{
			"name": ""
		}
		"""
	Then I validate that the response status code is "400"
