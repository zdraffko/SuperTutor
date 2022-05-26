import { Stack, Title } from "@mantine/core";
import PaymentsMakeItRainSvg from "./PaymentsMakeItRainSvg";

export const PaymentsDashboard: React.FC = () => (
    <Stack align="center" p="xl" justify="space-between" style={{ height: "95vh" }}>
        <Title>Изглежда все още нямаш осъществени плащания</Title>
        <PaymentsMakeItRainSvg />
    </Stack>
);
