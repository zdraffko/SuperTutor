import { Button, Stack, Text } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import useAcceptTermsOfService from "modules/payments/hooks/useAcceptTermsOfService";
import { useRouter } from "next/router";
import { useEffect } from "react";
import { Check, X } from "tabler-icons-react";
import { useAuth } from "utils/authentication/reactQueryAuth";

const AcceptTermsOfServiceStep: React.FC = () => {
    const { user } = useAuth();
    const router = useRouter();
    const {
        acceptTermsOfService,
        isAcceptTermsOfServiceFailed,
        isAcceptTermsOfServiceLoading,
        isAcceptTermsOfServiceSuccessful,
        acceptTermsOfServiceErrorMessage,
        resetAcceptTermsOfServiceRequestState
    } = useAcceptTermsOfService();

    useEffect(() => {
        if (isAcceptTermsOfServiceSuccessful) {
            showNotification({
                autoClose: 5000,
                message: "Формулярът за верификация бе завършен успешно",
                color: "teal",
                icon: <Check />
            });

            router.push("/payments");
        }

        if (isAcceptTermsOfServiceFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при запазването на данните",
                message: acceptTermsOfServiceErrorMessage,
                color: "red",
                icon: <X />
            });
        }

        resetAcceptTermsOfServiceRequestState();
    }, [acceptTermsOfServiceErrorMessage, isAcceptTermsOfServiceFailed, isAcceptTermsOfServiceSuccessful, resetAcceptTermsOfServiceRequestState, router]);

    return (
        <Stack align="center">
            <Text size="xl">Благодарим за попълнената информация</Text>
            <Text size="md">Ще получите обратна връзка относно верификацията в най-скоро време</Text>
            <Button onClick={async () => await acceptTermsOfService({ userId: user!.id })} loading={isAcceptTermsOfServiceLoading}>
                Приключи формуляра
            </Button>
            <Text size="xs" color="dimmed">
                С приключването на формуляра се съгласявате с условията на ползване не{" "}
                <Text size="xs" variant="link" component="a" target="_blank" href="https://stripe.com/en-bg/legal/connect-account">
                    Stripe
                </Text>
            </Text>
        </Stack>
    );
};
export default AcceptTermsOfServiceStep;
