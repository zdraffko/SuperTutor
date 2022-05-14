import { useMantineColorScheme } from "@mantine/core";
import { Tldraw } from "@tldraw/tldraw";
import React from "react";

export const Canvas: React.FC = () => {
    const { colorScheme } = useMantineColorScheme();

    return <Tldraw darkMode={colorScheme === "dark"} />;
};
