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
		| browser   | link        | page title        | page url                                                       |
		| PhantomJS | New         | New ExpenseReport | http://localhost:43507/ExpenseReport/Manage?mode=New           |
		| PhantomJS | Search      | Search Results    | http://localhost:43507/ExpenseReportSearch                     |
		| PhantomJS | My Expenses | Search Results    | http://localhost:43507/ExpenseReportSearch?Submitter=Assistant |