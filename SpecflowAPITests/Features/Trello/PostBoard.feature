Feature: PostBoard

@deleteTrelloBoard 
Scenario: Create Board
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
				"name": "Test automation Trello Board",			
				"desc" : "Board Description",
				"prefs_background": "lime",
				"prefs_cardAging": "pirate"
		}
		"""
	And I store board id for workspace cleaning
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PostBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath         | expectedValue                |
		| name             | Test automation Trello Board |
		| desc             | Board Description            |
		| prefs.background | lime                         |
		| prefs.cardAging  | pirate                       |