import { Center, Loader } from "@mantine/core";
import dynamic from "next/dynamic";

// From the documentation https://mantine.dev/others/rte/#usage-with-nextjs
export default dynamic(() => import("@mantine/rte"), {
    ssr: false,
    loading: () => (
        <Center style={{ width: "100%", height: "100%" }}>
            <Loader size="lg" />
        </Center>
    )
});
