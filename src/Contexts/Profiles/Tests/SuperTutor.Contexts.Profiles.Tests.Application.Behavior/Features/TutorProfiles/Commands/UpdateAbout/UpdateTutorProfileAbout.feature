Feature: As a tutor, I want to update my about text, in order to better represent myself in front of the students

Scenario: Updating a profile's about text when the profile is not inactive or for redaction
	Given the profile is not inactive or for redaction
	When the tutor tries to update his about text
	Then the about text should not be updated

Scenario: Updating a profile's about text when the new about text is empty
	Given the profile's new about text is empty
	And the profile is marked as inactive or for redaction
	When the tutor tries to update his about text
	Then the about text should not be updated

Scenario: Updating a profile's about text when the new about text is above the max allowed length
	Given the profile's new about text above the max allowed length
	And the profile is marked as inactive or for redaction
	When the tutor tries to update his about text
	Then the about text should not be updated

Scenario: Updating a profile's about text when the profile is inactive
	Given the profile is inactive
	And the new about text is valid
	When the tutor tries to update his about text
	Then the about text should be updated

Scenario: Updating a profile's about text when the profile is marked as for redaction
	Given the profile is marked as for redaction
	And the new about text is valid
	When the tutor tries to update his about text
	Then the about text should be updated
