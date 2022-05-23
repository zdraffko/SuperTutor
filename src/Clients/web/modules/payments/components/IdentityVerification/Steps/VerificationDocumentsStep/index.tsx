import { Center, Loader } from "@mantine/core";
import dynamic from "next/dynamic";

export default dynamic(() => import("./VerificationDocumentsStep"), {
    ssr: false,
    loading: () => (
        <Center style={{ width: "100%", height: "100%" }}>
            <Loader size="lg" />
        </Center>
    )
});
