import { ColorScheme, ColorSchemeProvider, Global, MantineProvider } from "@mantine/core";
import { useHotkeys, useLocalStorage } from "@mantine/hooks";
import { NotificationsProvider } from "@mantine/notifications";
import type { AppProps } from "next/app";

const App = ({ Component, pageProps }: AppProps) => {
    const [colorScheme, setColorScheme] = useLocalStorage<ColorScheme>({
        key: "color-scheme",
        defaultValue: "light",
        getInitialValueInEffect: true
    });

    const toggleColorScheme = (value?: ColorScheme) => setColorScheme(value || (colorScheme === "dark" ? "light" : "dark"));

    useHotkeys([["mod+Y", () => toggleColorScheme()]]);

    return (
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
                    <Component {...pageProps} />
                </NotificationsProvider>
            </MantineProvider>
        </ColorSchemeProvider>
    );
};

export default App;
