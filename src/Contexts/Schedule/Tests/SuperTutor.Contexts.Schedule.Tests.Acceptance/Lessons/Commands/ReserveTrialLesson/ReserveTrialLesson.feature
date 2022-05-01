Feature: As a student, I want to reserve a trial lesson with a tutor, in order to find out if the tutor is right for me

Scenario: Reserving a trial lesson when the selected date and time are in the past
	Given Alex has selected a passed date and time for the trial lesson
	When Alex tries to reserve the trial lesson
	Then the trial lesson should not be reserved

Scenario: Reserving a trial lesson when the selected date and time are in the future
	Given Alex has selected a date and time for the trial lesson that are in the future
	When Alex tries to reserve the trial lesson
	Then the trial lesson should be reserved

Scenario: Reserving a trial lesson when the selected date and time overlap with another lesson of the tutor
	Given Alex has selected a date and time for the trial lesson that overlaps with another lesson of the tutor
	When Alex tries to reserve the trial lesson
	Then the trial lesson should not be reserved

Scenario: Reserving a trial lesson when the selected date and time overlap with another lesson of the student
	Given Alex has selected a date and time for the trial lesson that overlaps with another lesson of his
	When Alex tries to reserve the trial lesson
	Then the trial lesson should not be reserved