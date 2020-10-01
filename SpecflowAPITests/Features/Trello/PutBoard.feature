Feature: Update Board

Background: Create board
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test automation CSharp"
		}
		"""
	And I store resource id for workspace cleaning
	And I store response "id" value as "BOARD_ID"
	Then I validate that the response status code is "200"


@deleteTrelloBoard
Scenario: Board is updated with name
	Given I use the "Trello" service client
	When I send a "Trello" PUT request to "boards/{BOARD_ID}" with the following json body
		"""
		{
			"name"					: "New board name",			
			"prefs/permissionLevel" :  "public",
			"prefs/background"		:  "pink",
			"prefs/cardAging"		:  "pirate"
		}
		"""
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PutBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath				| expectedValue		|
		| name					| New board name	|
		| prefs.permissionLevel	| public			|
		| prefs.background		| pink				|
		| prefs.cardAging		| pirate			|
