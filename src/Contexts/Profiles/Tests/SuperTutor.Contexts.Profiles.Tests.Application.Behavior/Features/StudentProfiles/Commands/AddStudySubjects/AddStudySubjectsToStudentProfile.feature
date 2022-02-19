Feature: As a student, I want to add a new study subjects to my profile, in order to get better tutor recommendations

Scenario: Adding a non already present study subject
	Given the study subject is not already present
	When the student tries to add the study subject to his profile
	Then the study subject should be added

Scenario: Adding an already present study subject
	Given the study subject is already present
	When the student tries to add the study subject to his profile
	Then the study subject should not be added

Scenario: Adding multiple study subject
	Given multiple study subjects
	When the student tries to add the study subjects to his profile
	Then only the not already present study subjects should be added
