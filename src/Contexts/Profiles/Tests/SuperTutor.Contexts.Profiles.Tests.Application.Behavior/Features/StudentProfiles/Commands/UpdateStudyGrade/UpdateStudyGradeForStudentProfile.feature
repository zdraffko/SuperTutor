Feature: As a student, I want to update my study grade, in order to get better tutor recommendations

Scenario: Updating the study grade
	Given a new study grade
	When the student tries to update the study grade of his profile
	Then the study grade should be updated to the new one
