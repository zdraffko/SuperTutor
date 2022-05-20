import { Badge, Center, Loader } from "@mantine/core";
import { Check } from "tabler-icons-react";

interface RoomIndicatorProps {
    classroomName: string;
    isClassroomSavingChanges: boolean;
}

const RoomIndicator: React.FC<RoomIndicatorProps> = ({ classroomName, isClassroomSavingChanges }) => (
    <Badge
        variant="outline"
        size="lg"
        leftSection={
            isClassroomSavingChanges ? (
                <Center>
                    <Loader size="xs" />
                </Center>
            ) : (
                <Center>
                    <Check />
                </Center>
            )
        }
    >
        {classroomName}
    </Badge>
);

export default RoomIndicator;
