Feature: As a student, I want to add a filter to my favorites, in order to easily perform the same search for tutors at a later time

Background:
	Given Alex is a student

Scenario: Adding a filter to favorites when the student already has the maximum number of filters in his favorites
	Given Alex has the maximum number of filters in his favorites
	When Alex tries add a new filter
	Then the new filter should not be added to Alex's favorites
	And the number of Alex's favorite filters should remain at the maximum

Scenario: Adding a filter to favorites when the same filter is already present in the student's favorites
	Given the same filter is already present in Alex's favorites
	When Alex tries add a new filter
	Then the new filter should not be added for a second time to Alex's favorites

Scenario: Adding a filter to favorites when the same filter is not already present in the student's favorites and the maximum number of filters in not reached by the student
	Given the same filter is not already present in Alex's favorites
	And Alex has not reached the maximum number of allowed favorite filters (he has '<NumberOfAlreadyAddedFavoriteFilters>')
	When Alex tries add a new filter
	Then the new filter should be added to Alex's favorites
Examples:
| NumberOfAlreadyAddedFavoriteFilters |
| 0                                   |
| 1                                   |
| 2                                   |
