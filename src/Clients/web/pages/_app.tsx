import { ColorScheme, ColorSchemeProvider, Global, MantineProvider } from "@mantine/core";
import { useHotkeys, useLocalStorage } from "@mantine/hooks";
import { NotificationsProvider } from "@mantine/notifications";
import type { AppProps } from "next/app";
import { QueryClientProvider } from "react-query";
import { ReactQueryDevtools } from "react-query/devtools";
import { AuthProvider } from "utils/authentication/reactQueryAuth";
import { queryClient } from "utils/reactQuery";

const App = ({ Component, pageProps }: AppProps) => {
    const [colorScheme, setColorScheme] = useLocalStorage<ColorScheme>({
        key: "color-scheme",
        defaultValue: "light",
        getInitialValueInEffect: true
    });

    const toggleColorScheme = (value?: ColorScheme) => setColorScheme(value || (colorScheme === "dark" ? "light" : "dark"));

    useHotkeys([["mod+Y", () => toggleColorScheme()]]);

    return (
        <QueryClientProvider client={queryClient}>
            <ColorSchemeProvider colorScheme={colorScheme} toggleColorScheme={toggleColorScheme}>
                <MantineProvider
                    theme={{
                        colorScheme,
                        primaryColor: "cyan"
                    }}
                    withGlobalStyles
                    withNormalizeCSS
                >
                    <NotificationsProvider>
                        <Global
                            styles={theme => ({
                                "*, *::before, *::after": {
                                    boxSizing: "border-box"
                                },
                                body: {
                                    ...theme.fn.fontStyles(),
                                    backgroundColor: theme.colorScheme === "dark" ? theme.colors.dark[9] : theme.colors.gray[0],
                                    color: theme.colorScheme === "dark" ? theme.colors.dark[0] : theme.black,
                                    lineHeight: theme.lineHeight,
                                    padding: 0,
                                    margin: 0
                                },
                                a: {
                                    color: "inherit",
                                    textDecoration: "none"
                                }
                            })}
                        />
                        <AuthProvider>
                            <Component {...pageProps} />
                        </AuthProvider>
                    </NotificationsProvider>
                </MantineProvider>
            </ColorSchemeProvider>
            <ReactQueryDevtools initialIsOpen={false} />
        </QueryClientProvider>
    );
};

export default App;
