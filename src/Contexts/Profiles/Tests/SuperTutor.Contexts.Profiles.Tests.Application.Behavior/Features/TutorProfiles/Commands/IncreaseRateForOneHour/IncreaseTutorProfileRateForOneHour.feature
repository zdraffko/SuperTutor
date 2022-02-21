Feature: As a tutor, I want to increase my rate for one hour, in order to increase my revenue

Scenario: Increasing the rate for one hour when the profile is not marked as inactive or for redaction
	Given the profile is not marked as inactive or for redaction
	When the tutor tries to increase the rate for one hour
	Then the rate for one hour should not be increased

Scenario: Increasing the rate for one hour when the increase amount is less than 1
	Given the increase amount is less than 1
	And the profile is marked as inactive or for redaction
	When the tutor tries to increase the rate for one hour
	Then the rate for one hour should not be increased

Scenario: Increasing the rate for one hour when the profile is marked as inactive
	Given the profile is marked as inactive
	And the increase amount is valid
	When the tutor tries to increase the rate for one hour
	Then the rate for one hour should be increased

Scenario: Increasing the rate for one hour when the profile is marked as for redaction
	Given the profile is marked as for redaction
	And the increase amount is valid
	When the tutor tries to increase the rate for one hour
	Then the rate for one hour should be increased
