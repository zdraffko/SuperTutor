Feature: As a tutor, I want to remove a tutoring grade from my profile, in order for my profile to be more easily discovered by students

Scenario: Removing a tutoring grade from a profile that is not inactive or for redaction
	Given the profile is not inactive or for redaction
	When the tutor tries to remove a tutoring grade from his profile
	Then the tutoring graded should not be removed

Scenario: Removing a present tutoring grade when the profile is inactive
	Given the tutoring grade for removal is present in the tutor's profile
	And the profile is inactive
	When the tutor tries to remove the tutoring grade from his profile
	Then the tutoring grade should be removed

Scenario: Removing a present tutoring grade when the profile is marked as for redaction
	Given the tutoring grade for removal is present in the tutor's profile
	And the profile is marked as for redaction
	When the tutor tries to remove the tutoring grade from his profile
	Then the tutoring grade should be removed

Scenario: Removing a non present tutoring grade when the profile is inactive
	Given the tutoring grade for removal is not present in the tutor's profile
	And the profile is inactive
	When the tutor tries to remove the tutoring grade from his profile
	Then none of the tutoring grades should be removed

Scenario: Removing a non present tutoring grade when the profile is marked as for redaction
	Given the tutoring grade for removal is not present in the tutor's profile
	And the profile is marked as for redaction
	When the tutor tries to remove the tutoring grade from his profile
	Then none of the tutoring grades should be removed

Scenario: Removing multiple study subject when the profile is inactive
	Given multiple study subjects for removal
	And the profile is inactive
	When the tutor tries to remove the tutoring grades from his profile
	Then only the present tutoring grades should be removed

Scenario: Removing multiple study subject when the profile is marked as for redaction
	Given multiple study subjects for removal
	And the profile is marked as for redaction
	When the tutor tries to remove the tutoring grades from his profile
	Then only the present tutoring grades should be removed

Scenario: Removing all tutoring grades from a profile
	Given all of the tutor's tutoring grades are for removal
	And the profile is marked as inactive or for redaction
	When the tutor tries to remove all of the tutoring grades from his profile
	Then none of the tutoring grades should be removed
