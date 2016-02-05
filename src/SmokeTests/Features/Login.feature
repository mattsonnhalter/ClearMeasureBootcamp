Feature: Login
	Log in page should be the first page the user sees if not logged in
	Home page should be the first page the user sees if logged in


Scenario Outline: Arrive at login page
	Given I am using <browser>
	When I browse to 'http://localhost:43507'
	And I am not logged in
	Then the page title should start with 'Login'
	And the page url should be exactly 'http://localhost:43507/Account/Login?ReturnUrl=%2F'

	Examples: 
			| browser			|
			| Firefox           |
			| Chrome            |
			| IE|

Scenario Outline: Arrive at home page
	Given I am using <browser>
	When I browse to 'http://localhost:43507'
	And I am logged in
	Then the page title should start with 'Home Page'
	And the page url should be exactly 'http://localhost:43507/'

	Examples: 
			| browser			|
			| Firefox           |
			| Chrome            |
			| IE |