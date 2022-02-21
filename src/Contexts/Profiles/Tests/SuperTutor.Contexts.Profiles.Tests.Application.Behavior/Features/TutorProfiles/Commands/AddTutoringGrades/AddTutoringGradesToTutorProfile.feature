Feature: As a tutor, I want to add a new tutoring grade to my profile, in order for my profile to be more easily discovered by students

Scenario: Adding a tutoring grade to a profile that is not inactive or for redaction
	Given the profile is not inactive or for redaction
	When the tutor tries to add the tutoring grade to his profile
	Then the tutoring graded should not be added

Scenario: Adding an already present tutoring grade when the profile is inactive
	Given the tutoring grade is already present
	And the profile is inactive
	When the tutor tries to add the tutoring grade to his profile
	Then the tutoring graded should not be added

Scenario: Adding an already present tutoring grade when the profile is marked as for redaction
	Given the tutoring grade is already present
	And the profile is marked as for redaction
	When the tutor tries to add the tutoring grade to his profile
	Then the tutoring graded should not be added

Scenario: Adding a non already present tutoring grade when the profile is inactive
	Given the tutoring grade is already present
	And the profile is inactive
	When the tutor tries to add the tutoring grade to his profile
	Then the tutoring graded should be added

Scenario: Adding a non already present tutoring grade when the profile is marked as for redaction
	Given the tutoring grade is already present
	And the profile is marked as for redaction
	When the tutor tries to add the tutoring grade to his profile
	Then the tutoring graded should be added

Scenario: Adding multiple tutoring grades when the profile is inactive
	Given multiple tutoring grades
	And the profile is inactive
	When the tutor tries to add the tutoring grades to his profile
	Then only the not already present tutoring grades should be added

Scenario: Adding multiple tutoring grades when the profile is marked as for redaction
	Given multiple tutoring grades
	And the profile is marked as for redaction
	When the tutor tries to add the tutoring grades to his profile
	Then only the not already present tutoring grades should be added
