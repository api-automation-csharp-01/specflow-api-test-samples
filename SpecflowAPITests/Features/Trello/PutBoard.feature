Feature: Update Project

Background: Create project
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test Board CSharp",
			"desc": "Project created with using steps definitions",
			"prefs_permissionLevel": "private",
			"prefs_voting": "members",
			"prefs_background": "green"
		}
		"""
	And I store project id for workspace cleaning
	And I store response "id" value as "PROJECT_ID"
	Then I validate that the response status code is "200"

@deleteTrelloProject
Scenario: Project is updated with name
	Given I use the "Trello" service client
	When I send a "Trello" PUT request to "boards/{PROJECT_ID}" with the following json body
		"""
		{
			"name": "New board name",
			"desc": "Description edited",
			"prefs_permissionLevel": "public",
			"prefs_background": "green"
		}
		"""
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PutBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath			    | expectedValue	       |
		| name				    | New board name	   |
		| desc				    | Description edited   |
		| prefs.permissionLevel | private		       |
		| prefs.background	    | green       	       |
