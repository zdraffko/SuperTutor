import { Center, Loader, Paper, Stack, Table, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import useGetTransfersForTutor from "modules/payments/hooks/useGetTransfersForTutor";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import PaymentsMakeItRainSvg from "./PaymentsMakeItRainSvg";

export const PaymentsDashboard: React.FC = () => {
    const { transfers, isGetTransfersForTutorFailed, isGetTransfersForTutorLoading, getTransfersForTutorErrorMessage } = useGetTransfersForTutor();

    useEffect(() => {
        if (isGetTransfersForTutorFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при вземането на плащаниятата",
                message: getTransfersForTutorErrorMessage,
                color: "red",
                icon: <X />
            });
        }
    }, [getTransfersForTutorErrorMessage, isGetTransfersForTutorFailed]);

    if (isGetTransfersForTutorLoading || !transfers) {
        return (
            <Center style={{ height: "50vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    if (transfers.length === 0) {
        return (
            <Stack align="center" justify="space-between" style={{ height: "95vh" }}>
                <Title>Изглежда все още нямаш осъществени плащания</Title>
                <PaymentsMakeItRainSvg />
            </Stack>
        );
    }

    return (
        <Paper m="xl" p="xl">
            <Stack align="center" style={{ height: "95vh" }}>
                <Title>Вашите плащания</Title>
                <Table>
                    <thead>
                        <tr>
                            <th>Номер</th>
                            <th>Сума</th>
                            <th>Валута</th>
                        </tr>
                    </thead>
                    <tbody>
                        {transfers.map((transfer, index) => (
                            <tr key={transfer.id}>
                                <td>{index + 1}</td>
                                <td>{transfer.amount}</td>
                                <td>лв.</td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            </Stack>
        </Paper>
    );
};
