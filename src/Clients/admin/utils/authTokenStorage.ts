const authTokenStorageKey = "supertutor_admin_auth_token";

const authTokenStorage = {
    get: (): string | null => window.localStorage.getItem(authTokenStorageKey),
    set: (token: string): void => window.localStorage.setItem(authTokenStorageKey, token),
    clear: (): void => window.localStorage.removeItem(authTokenStorageKey)
};

export default authTokenStorage;
