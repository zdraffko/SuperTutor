Feature: As an admin, I want to approve a tutor's profile, in order for the profile to be visible to students

Scenario: Approving a tutor profile that is not marked as for review
	Given the tutor profile is not marked as for review
	When the admin tries to approve the tutor profile
	Then the tutor profile should not be approved

Scenario: Approving a tutor profile without a redaction comment
	Given the tutor profile does not have a redaction comment
	And the tutor profile is marked as for review
	When the admin tries to approve the tutor profile
	Then the tutor profile should be approved

Scenario: Approving a tutor profile with a redaction comment
	Given the tutor profile has a redaction comment
	And the tutor profile is marked as for review
	When the admin tries to approve the tutor profile
	Then the tutor profile should be approved
	And the redaction comment should be settled with approvement
