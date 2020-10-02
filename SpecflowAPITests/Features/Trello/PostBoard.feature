Feature: Boards

@deleteTrelloBoard
Scenario: Create a Board
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Board from SpecFlow",
			"defaultLabels": "true",
			"defaulLists": "true", 
			"desc": "My Board 2020 SpecFlow", 
			"keepFromSource": "none", 
			"prefs_permissionLevel": "private", 
			"prefs_voting": "disabled", 
			"prefs_comments": "members", 
			"prefs_invitations": "members", 
			"prefs_selfJoin": "true", 
			"prefs_cardCovers": "true", 
			"prefs_background": "blue", 
			"prefs_cardAging": "regular"
		}
		"""
	And I store board id for workspace cleaning
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PostBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath       | expectedValue          |
		| name           | Board from SpecFlow    |
