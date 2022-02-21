Feature: As a tutor, I want to decrease my rate for one hour, in order for more students to be able to afford my services

Scenario: Decreasing the rate for one hour when the profile is not marked as inactive or for redaction
	Given the profile is not marked as inactive or for redaction
	When the tutor tries to decrease the rate for one hour
	Then the rate for one hour should not be decreased

Scenario: Decreasing the rate for one hour when the decrease amount is less than 1
	Given the decrease amount is less than 1
	And the profile is marked as inactive or for redaction
	When the tutor tries to decrease the rate for one hour
	Then the rate for one hour should not be decreased

Scenario: Decreasing the rate for one hour when the resulting rate for one hour is less than the minimum allowed
	Given the resulting rate for one hour is less than the minimum allowed
	And the profile is marked as inactive or for redaction
	When the tutor tries to decrease the rate for one hour
	Then the rate for one hour should not be decreased

Scenario: Decreasing the rate for one hour when the profile is marked as inactive
	Given the profile is marked as inactive
	And the decrease amount is valid
	When the tutor tries to decrease the rate for one hour
	Then the rate for one hour should be decreased

Scenario: Decreasing the rate for one hour when the profile is marked as for redaction
	Given the profile is marked as for redaction
	And the decrease amount is valid
	When the tutor tries to decrease the rate for one hour
	Then the rate for one hour should be decreased
