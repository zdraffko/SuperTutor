Feature: As a student, I want to add a filter to my favorites, in order to easily perform the same search for tutors at a later time

Scenario: Adding a filter to favorites when the student already has the maximum number of filters in his favorites
	Given the student has the maximum number of filters in his favorites
	When the student tries add a new filter
	Then the new filter should not be added to the student's favorites
	And the number of the students favorite filters should remain at the maximum

Scenario: Adding a filter to favorites when the same filter is already present in the student's favorites
	Given the same filter is already present in the student's favorites
	When the student tries add a new filter
	Then the new filter should not be added for a second time to the student's favorites

Scenario: Adding a filter to favorites when the same filter is not already present in the student's favorites and the maximum number of filters in not reached by the student
	Given the same filter is not already present in the student's favorites
	And the student has not reached the maximum number of allowed favorite filters (he has '<NumberOfAlreadyAddedFavoriteFilters>')
	When the student tries add a new filter
	Then the new filter should be added to the student's favorites
Examples:
| NumberOfAlreadyAddedFavoriteFilters |
| 0                                   |
| 1                                   |
| 2                                   |
