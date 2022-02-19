Feature: As a student, I want to create a profile, in order to find the perfect tutor for me

Scenario: Creating a profile with valid information
	Given the provided information for creating a profile is valid
	When the student tries to create a profile
	Then the profile should be created

Scenario: Creating a profile with invalid information
	Given the provided information for creating a profile is invalid
	When the student tries to create a profile
	Then the profile should not be created

Scenario: Creating a profile when the student already has one
	Given the student already has a profile
	When the student tries to create a profile
	Then the profile should not be created
