Feature: As a tutor, I want to deactivate my profile, in order for it to not be visible to students

Scenario: Deactivating a profile that is not marked as active or for review
	Given the profile is not marked as active or for review
	When the tutor tries to deactivate the profile
	Then the profile should not be deactivated

Scenario: Deactivating a profile that is marked as active
	Given the profile is marked as active
	When the tutor tries to deactivate the profile
	Then the profile should be deactivated

Scenario: Deactivating a profile that is marked as for review
	Given the profile is marked as fore review
	When the tutor tries to deactivate the profile
	Then the profile should be deactivated
