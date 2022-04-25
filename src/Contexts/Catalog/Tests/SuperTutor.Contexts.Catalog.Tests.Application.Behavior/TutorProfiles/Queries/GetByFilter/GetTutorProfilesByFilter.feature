Feature: As a student, I want to be able to use a filter when searching for a tutor, in order to get better search results

Background:
	Given There are tutor profiles in the catalog

Scenario: Getting tutor profiles by filtering the tutoring subject
	Given Alex selects '<TutoringSubjects>' as the tutoring subjects for the tutor profiles that he is searching for
	When Alex searches for tutor profiles
	Then only the profiles that have one of the specified tutoring subjects should be returned
Examples:
| TutoringSubjects |
| 1,2              |
| 0                |

Scenario: Getting tutor profiles by filtering the tutoring grades
	Given Alex selects '<TutoringGrades>' as the tutoring grades for the tutor profiles that he is searching for
	When Alex searches for tutor profiles
	Then only the profiles that have one of the specified tutoring grades should be returned
Examples:
| TutoringGrades |
| 9,10           |
| 11             |

Scenario: Getting tutor profiles by filtering the minimum rate for one hour
	Given Alex selects '<MinRateForOneHour>' as the minimum rate for one hour for the tutor profiles that he is searching for
	When Alex searches for tutor profiles
	Then only the profiles that have a rate for one hour that is above or equal to the one specified should be returned
Examples:
| MinRateForOneHour |
| 10                |
| 55.5              |
| 104               |

Scenario: Getting tutor profiles by filtering the maximum rate for one hour
	Given Alex selects '<MaxRateForOneHour>' as the maximum rate for one hour for the tutor profiles that he is searching for
	When Alex searches for tutor profiles
	Then only the profiles that have a rate for one hour that is below or equal to the one specified should be returned
Examples:
| MaxRateForOneHour |
| 10                |
| 55.5              |
| 104               |

Scenario: Getting tutor profiles when there are inactive profiles
	Given there are inactive tutor profiles
	When Alex searches for tutor profiles
	Then only the active tutor profiles should be returned