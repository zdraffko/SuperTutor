const authTokenStorageKey = "supertutor_auth_token";

const authTokenStorage = {
    get: (): string | null => {
        const rawAuthToken = window.localStorage.getItem(authTokenStorageKey);
        if (rawAuthToken === null) {
            return null;
        }

        return JSON.parse(rawAuthToken);
    },
    set: (token: string): void => window.localStorage.setItem(authTokenStorageKey, JSON.stringify(token)),
    clear: (): void => window.localStorage.removeItem(authTokenStorageKey)
};

export default authTokenStorage;
