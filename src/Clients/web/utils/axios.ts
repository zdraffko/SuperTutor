import Axios, { AxiosRequestConfig } from "axios";
import getConfig from "next/config";
import authTokenStorage from "./authTokenStorage";

const authenticationInterceptor = (config: AxiosRequestConfig) => {
    const authToken = authTokenStorage.get();

    if (authToken !== null && config.headers !== undefined) {
        config.headers.authorization = `Bearer ${authToken}`;
    }

    return config;
};

const { publicRuntimeConfig } = getConfig();

export const axios = Axios.create({
    baseURL: publicRuntimeConfig.apiUrl
});

axios.interceptors.request.use(authenticationInterceptor);
