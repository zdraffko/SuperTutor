import { Center, Loader } from "@mantine/core";
import dynamic from "next/dynamic";

// This file is required because VerificationDocumentsStep cannot be sever-side rendered because it uses the File type that is for browsers only
export default dynamic(() => import("./VerificationDocumentsStep"), {
    ssr: false,
    loading: () => (
        <Center style={{ width: "100%", height: "100%" }}>
            <Loader size="lg" />
        </Center>
    )
});
