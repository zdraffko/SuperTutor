import { Button, Center, Loader, Paper, Space, Title } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { PaymentElement, useElements, useStripe } from "@stripe/react-stripe-js";
import { FormEvent, useState } from "react";
import { X } from "tabler-icons-react";

export const Checkout: React.FC = () => {
    const stripe = useStripe();
    const elements = useElements();

    const [isCheckoutStarted, setIsCheckoutStarted] = useState(false);

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        setIsCheckoutStarted(true);

        // We don't want to let default form submission happen here,
        // which would refresh the page.
        event.preventDefault();

        if (!stripe || !elements) {
            // Stripe.js has not yet loaded.
            // Make sure to disable form submission until Stripe.js has loaded.
            return;
        }

        const { error } = await stripe.confirmPayment({
            //`Elements` instance that was used to create the Payment Element
            elements,
            confirmParams: {
                return_url: "http://localhost:3000/payments/pay/result"
            }
        });

        if (error) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при плащането на урокът",
                message: error.message,
                color: "red",
                icon: <X />
            });

            // This point will only be reached if there is an immediate error when
            // confirming the payment. Show error to your customer (for example, payment
            // details incomplete)
        }
    };

    return (
        <Center style={{ height: "80vh" }}>
            <Paper p="xl" mt="xl" style={{ width: "50vw" }}>
                <form
                    onSubmit={async event => {
                        await handleSubmit(event);
                    }}
                >
                    <Title align="center">Урокът е запазен!</Title>
                    <Title order={4} align="center" mb="xl">
                        Направи плащането за да го потвърдиш
                    </Title>
                    <Space h="xl" />
                    {stripe ? <PaymentElement /> : <Loader size="lg" />}
                    <Space h="xl" />
                    <Center>
                        <Button mt="xl" type="submit" fullWidth size="sm" style={{ width: "50%" }} loading={isCheckoutStarted || !stripe}>
                            Плати урокът
                        </Button>
                    </Center>
                </form>
            </Paper>
        </Center>
    );
};
