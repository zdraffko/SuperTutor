Feature: As a tutor, I want to add availability to my schedule, in order to allow new students to instantly book a lesson with me without needing to contact me first

Scenario: Adding availability when the selected date and time are in the past
	Given Marti has selected a passed date and time for his availability time slot
	When Marti tries to add availability
	Then the new availability time slot should not be added

Scenario: Adding availability when the selected date and time are not at least 30 minutes into the feature
	Given Marti has selected a date and time that are less than 30 minutes into the feature for his availability time slot
	When Marti tries to add availability
	Then the new availability time slot should not be added

Scenario: Adding availability when the selected date and time are at least 30 minutes into the feature
	Given Marti has selected a date and time that are at least 30 minutes into the feature for his availability time slot
	When Marti tries to add availability
	Then the new availability time slot should be added

Scenario: Adding availability when the new availability time slot overlaps with the preceding time slot
	Given Marti has selected a date and time for the new availability time slot that overlaps with the preceding time slot
	When Marti tries to add availability
	Then the new availability time slot should not be added

Scenario: Adding availability when the new availability time slot overlaps with the subsequent time slot
	Given Marti has selected a date and time for the new availability time slot that overlaps with the subsequent time slot
	When Marti tries to add availability
	Then the new availability time slot should not be added
