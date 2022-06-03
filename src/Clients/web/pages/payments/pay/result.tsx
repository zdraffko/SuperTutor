import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { CheckoutResult } from "modules/payments";
import { NextPage } from "next";
import getConfig from "next/config";

const { publicRuntimeConfig } = getConfig();
const stripe = loadStripe(publicRuntimeConfig.stripePublicKey);

const CheckoutResultPage: NextPage = () => (
    <AuthenticationProtectedPage>
        <MainLayout>
            <Elements stripe={stripe}>
                <CheckoutResult />
            </Elements>
        </MainLayout>
    </AuthenticationProtectedPage>
);

export default CheckoutResultPage;
