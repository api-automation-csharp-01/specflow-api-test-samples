﻿Feature: PostBoard

A short summary of the feature

@deleteTrelloBoard
Scenario: Board is created with name
	Given I use the "Trello" service client
	When I send a "Trello" POST request to "boards" with the following json body
		"""
		{
			"name": "Test automation CSharp"
		}
		"""
	And I store project id for workspace cleaning
	Then I validate that the response status code is "200"
	And I validate that the response body match "Schemas/Trello/PostBoardSchema.json" JSON schema
	And I validate that the response body contains the following values
		| jsonpath       | expectedValue          |
		| name           | Test automation CSharp |
	#	| kind           | project                |
	#	| week_start_day | Monday                 |