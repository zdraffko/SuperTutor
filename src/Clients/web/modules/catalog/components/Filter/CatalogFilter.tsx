import { Button, Group, MultiSelect, NumberInput, Paper, Select } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import useGetTutorProfilesByFilter from "modules/catalog/hooks/useGetTutorProfilesByFilter";
import { CatalogTutorProfile } from "modules/catalog/types";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { X } from "tabler-icons-react";
import { tutoringGrades } from "types/tutoringGrades";
import { tutoringSubjects } from "types/tutoringSubjects";

interface CatalogFilterProps {
    setTutorProfiles: Dispatch<SetStateAction<CatalogTutorProfile[]>>;
}

const CatalogFilter: React.FC<CatalogFilterProps> = ({ setTutorProfiles }) => {
    const [tutoringSubjectFilter, setTutoringSubjectFilter] = useState<string | null>(null);
    const [tutoringGradesFilter, setTutoringGradesFilter] = useState<string[]>();
    const [minRateForOneHourFilter, setMinRateForOneHourFilter] = useState<number>();
    const [maxRateForOneHourFilter, setMaxRateForOneHourFilter] = useState<number>();

    const tutoringSubjectSelectData = tutoringSubjects.map(tutoringSubject => ({
        value: tutoringSubject.value.toString(),
        label: tutoringSubject.name
    }));

    const tutoringGradesSelectData = tutoringGrades.map(tutoringGrade => ({
        value: tutoringGrade.value.toString(),
        label: tutoringGrade.name
    }));

    const { tutorProfiles, refetchTutorProfiles, isGetTutorProfilesByFilterFailed, isGetTutorProfilesByFilterLoading, isGetTutorProfilesByFilterSuccessful, getTutorProfilesByFilterErrorMessage } =
        useGetTutorProfilesByFilter({
            tutoringSubject: tutoringSubjectFilter,
            tutoringGrades: tutoringGradesFilter ?? null,
            minRateForOneHour: minRateForOneHourFilter ?? null,
            maxRateForOneHour: maxRateForOneHourFilter ?? null
        });

    useEffect(() => {
        refetchTutorProfiles();
    }, [refetchTutorProfiles]);

    useEffect(() => {
        if (isGetTutorProfilesByFilterSuccessful && tutorProfiles) {
            setTutorProfiles(tutorProfiles);
        }

        if (isGetTutorProfilesByFilterFailed) {
            showNotification({
                autoClose: 5000,
                title: "???????????????? ?????????????? ?????? ?????????????????? ???? ?????????????????? ??????????????",
                message: getTutorProfilesByFilterErrorMessage,
                color: "red",
                icon: <X />
            });
        }
    }, [getTutorProfilesByFilterErrorMessage, isGetTutorProfilesByFilterFailed, isGetTutorProfilesByFilterSuccessful, setTutorProfiles, tutorProfiles]);

    return (
        <Paper withBorder shadow="sm" pl="xl" pr="xl" pt="md" pb="xl" m="xl">
            <Group position="apart">
                <Group>
                    <Select
                        searchable
                        clearable
                        value={tutoringSubjectFilter}
                        onChange={setTutoringSubjectFilter}
                        disabled={isGetTutorProfilesByFilterLoading}
                        label="??????????????"
                        placeholder="???????????? ??????????????"
                        data={tutoringSubjectSelectData}
                    />
                    <MultiSelect
                        value={tutoringGradesFilter}
                        onChange={setTutoringGradesFilter}
                        disabled={isGetTutorProfilesByFilterLoading}
                        data={tutoringGradesSelectData}
                        label="????????"
                        placeholder="???????????? ????????"
                    />
                    <NumberInput value={minRateForOneHourFilter} onChange={setMinRateForOneHourFilter} disabled={isGetTutorProfilesByFilterLoading} label="?????????????????? ???????? ???? ??????" />
                    <NumberInput value={maxRateForOneHourFilter} onChange={setMaxRateForOneHourFilter} disabled={isGetTutorProfilesByFilterLoading} label="???????????????????? ???????? ???? ??????" />
                </Group>
                <Button size="sm" loading={isGetTutorProfilesByFilterLoading} onClick={() => refetchTutorProfiles()}>
                    ??????????
                </Button>
            </Group>
        </Paper>
    );
};

export default CatalogFilter;
