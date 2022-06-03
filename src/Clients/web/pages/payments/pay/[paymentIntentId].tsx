import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { Checkout } from "modules/payments";
import { NextPage } from "next";
import getConfig from "next/config";
import { useRouter } from "next/router";

const { publicRuntimeConfig } = getConfig();
const stripe = loadStripe(publicRuntimeConfig.stripePublicKey);

const CheckoutPage: NextPage = () => {
    const router = useRouter();
    const { paymentIntentId } = router.query;

    const options = {
        clientSecret: typeof paymentIntentId === "string" ? paymentIntentId : ""
    };

    return (
        <AuthenticationProtectedPage>
            <MainLayout>
                <Elements stripe={stripe} options={options}>
                    <Checkout />
                </Elements>
            </MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default CheckoutPage;
