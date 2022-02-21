Feature: As a tutor, I want to create a profile, in order for students to be able to find me

Scenario: Creating a profile with valid information
	Given the provided information for creating a profile is valid
	When the tutor tries to create a profile
	Then the profile should be created

Scenario: Creating a profile with invalid information
	Given the provided information for creating a profile is invalid
	When the tutor tries to create a profile
	Then the profile should not be created

Scenario: Creating a profile when the tutor already has one with the same tutoring subject
	Given the tutor already has a profile with the same tutoring subject
	When the tutor tries to create a profile
	Then the profile should not be created
