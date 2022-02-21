Feature: As a tutor, I want to delete my profile, in order for the information in that profile to not be stored anywhere

Scenario: Deleting a profile
	When the tutor tries to delete his profile
	Then the profile should be deleted