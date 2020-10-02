Feature: Create Board

@deleteTrelloBoard
Scenario: Board is created with name
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test Board2",
			"desc": "Project created with using steps definitions",
			"prefs_permissionLevel": "private",
			"prefs_voting": "members",
			"prefs_background": "green"
		}
		"""
	And I store board id for workspace cleaning
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PostBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath				| expectedValue								   |
		| name				    | Test Board								   |
		| desc				    | Project created with using steps definitions |
		| prefs.permissionLevel | private									   |
		| prefs.voting		    | members									   |
		| prefs.backgroung	    | green  									   |

Scenario: Board is created with empty name
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": ""
		}
		"""
	Then I validate that the response status code is "400"
