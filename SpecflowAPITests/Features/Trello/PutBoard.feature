Feature: Update Board

Background: Create Board
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test automation CSharp Trello",			
			"prefs_background": "lime"		
		}
		"""
	And I store board id for workspace cleaning
	And I store response "id" value as "BOARD_ID"
	Then I validate that the response status code is "200"

@deleteTrelloBoard
Scenario: Update Board
	Given I use the "Trello" service client
	When I send a "Trello" PUT request to "boards/{BOARD_ID}" with the following json body
		"""
		{
			"name": "Test automation CSharp Trello UPDATED",
			"prefs/background": "red"   
		}
		"""
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PutBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath         | expectedValue                         |
		| name             | Test automation CSharp Trello UPDATED |
		| prefs.background | red                                   |
