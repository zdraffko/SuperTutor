Feature: As a tutor, I want to take time off in my schedule, in order to notify students that I will be unavailable during that time

Scenario: Taking time off when the selected date and time are in the past
	Given Marti has selected a passed date and time for his time off time slot
	When Marti tries to take time off
	Then the new time off time slot should not be added

Scenario: Taking time off when the selected date and time are not at least 30 minutes into the feature
	Given Marti has selected a date and time that are less than 30 minutes into the feature for his time off time slot
	When Marti tries to take time off
	Then the new time off time slot should not be added

Scenario: Taking time off when the selected date and time are at least 30 minutes into the feature
	Given Marti has selected a date and time that are at least 30 minutes into the feature for his time off time slot
	When Marti tries to take time off
	Then the new time off time slot should be added

Scenario: Taking time off when the new availability time slot overlaps with the preceding time slot
	Given Marti has selected a date and time for the new time off time slot that overlaps with the preceding time slot
	When Marti tries to take time off
	Then the new time off time slot should not be added

Scenario: Taking time off when the new availability time slot overlaps with the subsequent time slot
	Given Marti has selected a date and time for the new time off time slot that overlaps with the subsequent time slot
	When Marti tries to take time off
	Then the new time off time slot should not be added
