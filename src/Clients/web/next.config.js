/** @type {import('next').NextConfig} */

const nextConfig = {
    reactStrictMode: false, // TODO - This should be set to true. Currently it is set to false because strict mode renders components twice to detect problems but that was causing issues with the current implementation of the signalr/simple-peer flow.
    serverRuntimeConfig: {},
    publicRuntimeConfig: {
        apiUrl: process.env.API_URL,
        stripePublicKey: process.env.STRIPE_PUBLIC_KEY
    }
};

module.exports = nextConfig;
