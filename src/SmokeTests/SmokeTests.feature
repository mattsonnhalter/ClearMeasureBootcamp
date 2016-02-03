Feature: Smoke test
	In order to avoid breaking the site
	As a developer
	I want to check that the main pages are functional

Scenario: Search the Google
	Given I am using Firefox
	When I browse to 'https://google.com'
	 And I search for 'tofu'
	Then the page title should start with 'tofu'

Scenario: Browse to Clear Measure
    Given I am using Firefox
	When I browse to 'http://www.clear-measure.com'
    Then the page title should start with 'Clear Measure - Business Critical Custom Software'
