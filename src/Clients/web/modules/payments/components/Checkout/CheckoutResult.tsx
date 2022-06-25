import { Button, Center, Loader, Paper, Title } from "@mantine/core";
import { useStripe } from "@stripe/react-stripe-js";
import Link from "next/link";
import { useEffect, useState } from "react";

export const CheckoutResult: React.FC = () => {
    const stripe = useStripe();
    const [isPaymentSuccessful, setIsPaymentSuccessful] = useState(false);
    const [isPaymentResultLoaded, setIsPaymentResultLoaded] = useState(false);

    useEffect(() => {
        if (!stripe) {
            return;
        }

        // Retrieve the "payment_intent_client_secret" query parameter appended to
        // your return_url by Stripe.js
        const clientSecret = new URLSearchParams(window.location.search).get("payment_intent_client_secret");

        if (!clientSecret) {
            return;
        }

        // Retrieve the PaymentIntent
        stripe.retrievePaymentIntent(clientSecret).then(({ paymentIntent }) => {
            setIsPaymentResultLoaded(true);
            // Inspect the PaymentIntent `status` to indicate the status of the payment
            // to your customer.
            //
            // Some payment methods will [immediately succeed or fail][0] upon
            // confirmation, while others will first enter a `processing` state.
            //
            // [0]: https://stripe.com/docs/payments/payment-methods#payment-notification

            switch (paymentIntent?.status) {
                case "succeeded":
                    setIsPaymentSuccessful(true);
                    break;

                case "processing":
                    setIsPaymentSuccessful(true);
                    break;

                case "requires_payment_method":
                    // Redirect your user back to your payment page to attempt collecting
                    // payment again
                    setIsPaymentSuccessful(false);
                    break;

                default:
                    setIsPaymentSuccessful(false);
                    break;
            }
        });
    }, [stripe]);

    return (
        <Center style={{ height: "80vh" }}>
            {isPaymentResultLoaded ? (
                <Paper p="xl" mt="xl" style={{ width: "50vw" }}>
                    {isPaymentSuccessful ? (
                        <>
                            <Title align="center">Плащането бе успешно</Title>
                            <Title order={4} align="center" mb="xl">
                                Може да напуснете тази страница
                            </Title>
                        </>
                    ) : (
                        <>
                            <Title align="center">Плащането не бе успешно</Title>
                            <Title order={4} align="center" mb="xl">
                                Моля опитайте пак
                            </Title>
                        </>
                    )}
                    <Center>
                        <Link href="/dashboard">
                            <Button mt="xl" type="submit" fullWidth size="sm" style={{ width: "50%" }} loading={!stripe}>
                                Към началното табло
                            </Button>
                        </Link>
                    </Center>
                </Paper>
            ) : (
                <Loader size="xl" />
            )}
        </Center>
    );
};
