Feature: As a tutor, I want to submit my profile for review, in order for my profile to be approved by an admin so that it can be activated

Scenario: Submitting a profile for review when the profile is not inactive or for redaction
	Given the profile is not inactive or for redaction
	When the tutor tries to submit his profile for review
	Then the profile should not be submitted for review

Scenario: Submitting a profile for review when the profile has not been modified since the last redaction request
	Given the profile has not been modified since the last redaction request
	And the profile is marked as inactive or for redaction
	When the tutor tries to submit his profile for review
	Then the profile should not be submitted for review

Scenario: Submitting a profile for review when the profile is inactive
	Given the profile is inactive
	When the tutor tries to submit his profile for review
	Then the profile should be submitted for review

Scenario: Submitting a profile for review when the profile is marked as for redaction
	Given the profile is marked as for redaction
	When the tutor tries to submit his profile for review
	Then the profile should be submitted for review
