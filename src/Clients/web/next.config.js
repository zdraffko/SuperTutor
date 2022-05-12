/** @type {import('next').NextConfig} */

const nextConfig = {
    reactStrictMode: true,
    serverRuntimeConfig: {},
    publicRuntimeConfig: {
        apiUrl: process.env.API_URL
    }
};

module.exports = nextConfig;
