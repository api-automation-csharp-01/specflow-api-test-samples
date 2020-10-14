Feature: Create Board


Scenario: Board piblic is created with name
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
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PostBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath              | expectedValue     |
		| name                  | first board       |
		| desc                  | first description |
		| closed                | False             |
		| pinned                | False             |
		| prefs.background      | sky               |
		| prefs.permissionLevel | public            |
					