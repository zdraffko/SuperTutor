Feature: As a tutor, I want to remove time off from my schedule, in order to keep my schedule up to date

Scenario: Removing time off when the selected time slot is not of type 'time off'
	Given Marti has selected a time slot for removal that is not of type 'time off'
	When Marti tries to remove the time slot
	Then the time slot should not be removed

Scenario: Removing time off when the selected time slot is of type 'time off'
	Given Marti has selected a time slot for removal that is not of type 'time off'
	When Marti tries to remove the time slot
	Then the time slot should be removed
