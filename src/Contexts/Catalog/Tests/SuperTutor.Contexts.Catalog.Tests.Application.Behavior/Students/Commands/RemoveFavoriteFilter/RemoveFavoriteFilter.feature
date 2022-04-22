Feature: As a student, I want to remove a filter that I no longer need from my favorites, in order to free up space in my favorites for a new filter

Background:
	Given Alex is a student that wants to remove a favorite filter

Scenario: Removing an existing filter from favorites
	Given Alex has a filter in his favorites
	When Alex tries to remove that filter
	Then the filter should be removed from his favorites

Scenario: Removing a non existing filter from favorites
	Given Alex has a filter in his favorites
	When Alex tries to remove a non existing filter from his favorites
	Then non of his filters should be removed
