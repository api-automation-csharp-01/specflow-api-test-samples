Feature: Create board

@deleteTrelloBoard
Scenario: Board is created with name
	Given  I use the "Trello" service client
	When I send a POST request to "https://api.trello.com/1/boards/" with the following body
	"""
	{
		"name":"Board test01",
		"desc":"Created by automation testing"
	}
	"""
	And I store board id for workstapce cleaning
	Then I validate that status code returned is "200"
	And I validate that the response body match "Schemas/PostSchema.json" JSON schema
	And I validate that response body contains the following values
	| jsonpath | expectedValue                   |
	|  name    | Board test01                    |
	|  desc    | Created by automation testing   |

