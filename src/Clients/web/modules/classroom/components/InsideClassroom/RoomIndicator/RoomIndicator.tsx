import { Badge, Center, Loader } from "@mantine/core";
import { Check } from "tabler-icons-react";

interface RoomIndicatorProps {
    isClassroomSavingChanges: boolean;
}

const RoomIndicator: React.FC<RoomIndicatorProps> = ({ isClassroomSavingChanges }) => (
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
    ></Badge>
);

export default RoomIndicator;
