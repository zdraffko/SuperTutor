Feature: As a tutor, I want to activate my profile, in order for it to be visible to students

Scenario: Activating an inactive profile
	Given the profile is inactive
	When the tutor tries to activate the profile
	Then the profile should be activated

Scenario: Activating a profile that is not inactive
	Given the profile is not inactive
	When the tutor tries to activate the profile
	Then the profile should be activated

Scenario: Activating a profile that has never been approved by an admin
	Given the profile has never been approved by an admin
	And the profile is inactive
	When the tutor tries to activate the profile
	Then the profile should not be activated

Scenario: Activating a profile that has been modified since the last approval by an admin
	Given the profile has been modified since the last approval by an admin
	And the profile is inactive
	When the tutor tries to activate the profile
	Then the profile should not be activated