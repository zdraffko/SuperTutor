Feature: As a tutor, I want to remove availability from my schedule, in order to keep my schedule up to date

Scenario: Removing availability when the selected time slot is not of type 'availability'
	Given Marti has selected a time slot for removal that is not of type 'availability'
	When Marti tries to remove the time slot
	Then the time slot should not be removed

Scenario: Removing time off when the selected time slot is of type 'availability'
	Given Marti has selected a time slot for removal that is not of type 'availability'
	When Marti tries to remove the time slot
	Then the time slot should be removed
