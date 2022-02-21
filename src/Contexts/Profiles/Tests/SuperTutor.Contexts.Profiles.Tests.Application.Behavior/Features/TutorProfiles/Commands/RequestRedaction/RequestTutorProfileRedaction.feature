Feature: As an admin, I want to request redaction on a tutor's profile, in order for the information in the profile to be amended

Scenario: Requesting redaction on a tutor profile that is not marked as for review
	Given the tutor profile is not marked as for review
	When the admin tries to request redaction on the tutor profile
	Then a redaction should not be requested on the tutor's profile

Scenario: Requesting redaction on a tutor profile that is marked as for review
	Given the tutor profile is marked as for review
	When the admin tries to request redaction on the tutor profile
	Then a redaction should be requested on the tutor's profile
	And a new redaction comment should be added for the tutor's profile

Scenario: Requesting redaction when the tutor profile has a redaction comment
	Given the tutor profile has a redaction comment
	And the tutor profile is marked as for review
	When the admin tries to request redaction on the tutor profile
	Then a redaction should be requested on the tutor's profile
	And a new redaction comment should be added for the tutor's profile
	And the old redaction comment should be settled with new redaction request
