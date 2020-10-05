@deleteTrelloBoard
Feature: Create Board

Background: Create board
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test duplicated"
		}
		"""
	And I store board id for workspace cleaning
	Then I validate that the response status code is "200"

Scenario: Board is created with basic fields
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test automation CSharp",
			"desc": "First Board for Automation API task",
			"prefs_permissionLevel": "public",
			"prefs_background": "green"
		}
		"""
	And I store board id for workspace cleaning
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PostBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath              | expectedValue                       |
		| name                  | Test automation CSharp              |
		| desc                  | First Board for Automation API task |
		| prefs.permissionLevel | public                              |
		| prefs.background      | green                               |


Scenario: Board is not created with empty name
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": ""
		}
		"""
	Then I validate that the response status code is "400"	

Scenario: Board is created with duplicated name
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test duplicated",
			"desc": "Second Board for Automation API task",
			"prefs_permissionLevel": "public",
			"prefs_background": "green"
		}
		"""
	And I store board id for workspace cleaning
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PostBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath              | expectedValue                        |
		| name                  | Test duplicated                      |
		| desc                  | Second Board for Automation API task |
		| prefs.permissionLevel | public                               |
		| prefs.background      | green                                |