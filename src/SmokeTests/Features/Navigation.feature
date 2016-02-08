Feature: Navigation
	All links on the navigation bar should take me to the correct page

Scenario Outline: Link to correct page
	Given I am using <browser>
	And I am logged in on 'http://localhost:43507'
	And I am on the home page
	When I click on the <link> link
	Then the page title should start with <page title>
	And the page url should be exactly <page url>

	Examples: 
		| browser | link        | page title        | page url                                                       |
		| Firefox | New         | New ExpenseReport | http://localhost:43507/ExpenseReport/Manage?mode=New           |
		| Firefox | Search      | Search Results    | http://localhost:43507/ExpenseReportSearch                     |
		| Firefox | My Expenses | Search Results    | http://localhost:43507/ExpenseReportSearch?Submitter=Assistant |
		| Chrome  | New         | New ExpenseReport | http://localhost:43507/ExpenseReport/Manage?mode=New           |
		| Chrome  | Search      | Search Results    | http://localhost:43507/ExpenseReportSearch                     |
		| Chrome  | My Expenses | Search Results    | http://localhost:43507/ExpenseReportSearch?Submitter=Assistant |
		| IE      | New         | New ExpenseReport | http://localhost:43507/ExpenseReport/Manage?mode=New           |
		| IE      | Search      | Search Results    | http://localhost:43507/ExpenseReportSearch                     |
		| IE      | My Expenses | Search Results    | http://localhost:43507/ExpenseReportSearch?Submitter=Assistant |