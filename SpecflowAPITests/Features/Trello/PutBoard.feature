Feature: Update Board

Background: Create Board
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "first board",
			"desc": "first description",
			"closed": "false",
			"pinned": "false",
			"prefs_background": "sky",
			"prefs_permissionLevel": "public"
		}
		"""
	And I store project id for workspace cleaning
	And I store response "id" value as "BOARD_ID"
	Then I validate that the response status code is "200"

@deleteBoard
Scenario: Board is updated with name, description, background color and permissions
	Given I use the "Trello" service client
	When I send a "Trello" PUT request to "boards/{BOARD_ID}" with the following json body
		"""
		{
			"name": "first board updated",
			"desc": "first description updated",
			"prefs/background": "purple",
			"prefs/permissionLevel": "private"
		}
		"""
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PutBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath              | expectedValue             |
		| name                  | first board updated       |
		| desc                  | first description updated |
		| prefs.background      | purple                    |
		| prefs.permissionLevel | private                   |
						