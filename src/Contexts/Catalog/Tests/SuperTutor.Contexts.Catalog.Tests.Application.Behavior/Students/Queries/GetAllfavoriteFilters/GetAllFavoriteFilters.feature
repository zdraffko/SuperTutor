Feature: As a student, I want to be able to get all of my favorite filters, in order to use one of them for repeating a search that I have already done

Background:
	Given Alex is a student

Scenario: Getting all of the favorite filters for a student when he does not have any
	Given Alex does not have any favorite filters
	When Alex tries to get all of his favorite filters
	Then no favorite filters should be returned

Scenario: Getting all of the favorite filters for a student when he has some
	Given Alex has favorite filters
	When Alex tries to get all of his favorite filters
	Then all of Alex's favorite filters should be returned
