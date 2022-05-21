/** @type {import('next').NextConfig} */

const nextConfig = {
    reactStrictMode: false,
    serverRuntimeConfig: {},
    publicRuntimeConfig: {
        apiUrl: process.env.API_URL
    }
};

module.exports = nextConfig;
