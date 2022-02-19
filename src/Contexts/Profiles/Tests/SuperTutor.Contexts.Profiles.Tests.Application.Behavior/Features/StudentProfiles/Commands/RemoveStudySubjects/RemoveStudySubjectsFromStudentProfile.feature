Feature: As a student, I want to remove a study subjects from my profile, in order to get better tutor recommendations

Scenario: Removing a present study subject
	Given the study subject for removal is present in the student's profile
	When the student tries to remove the study subject from his profile
	Then the study subject should be removed

Scenario: Removing a non present study subject
	Given the study subject for removal is not present in the student's profile
	When the student tries to remove the study subject from his profile
	Then non of the study subjects should be removed

Scenario: Removing multiple study subject
	Given multiple study subjects for removal
	When the student tries to remove the study subjects from his profile
	Then only the present study subjects should be removed

Scenario: Removing all of the present study subject
	Given all of the student's study subjects are for removal
	When the student tries to remove all of the study subjects from his profile
	Then non of the study subjects should be removed
