import { Center, Loader } from "@mantine/core";
import AuthenticationProtectedPage from "components/AuthenticationProtectedPage";
import MainLayout from "components/MainLayout";
import { IdentityVerification } from "modules/payments";
import { PaymentsDashboard } from "modules/payments/components/Dashboard/PaymentsDashboard";
import useGetAreTutorTermsOfServiceAccepted from "modules/payments/hooks/useGetAreTutorTermsOfServiceAccepted";
import useGetAreTutorVerificationDocumentsCollected from "modules/payments/hooks/useGetAreTutorVerificationDocumentsCollected";
import useGetIsTutorAddressInformationCollected from "modules/payments/hooks/useGetIsTutorAddressInformationCollected";
import useGetIsTutorBankAccountInformationCollected from "modules/payments/hooks/useGetIsTutorBankAccountInformationCollected";
import useGetIsTutorPersonalInformationCollected from "modules/payments/hooks/useGetIsTutorPersonalInformationCollected";
import { NextPage } from "next";

const PaymentsPage: NextPage = () => {
    const { areTutorTermsOfServiceAccepted, isGetAreTutorTermsOfServiceAcceptedLoading } = useGetAreTutorTermsOfServiceAccepted();
    const { areVerificationDocumentsCollected, isGetAreVerificationDocumentsCollectedLoading } = useGetAreTutorVerificationDocumentsCollected();
    const { isTutorAddressInformationCollected, isGetIsTutorAddressInformationCollectedLoading } = useGetIsTutorAddressInformationCollected();
    const { isTutorBankAccountInformationCollected, isGetIsTutorBankAccountInformationCollectedLoading } = useGetIsTutorBankAccountInformationCollected();
    const { isTutorPersonalInformationCollected, isGetIsTutorPersonalInformationCollectedLoading } = useGetIsTutorPersonalInformationCollected();

    const isIdentityVerificationInformationCollected =
        areTutorTermsOfServiceAccepted && areVerificationDocumentsCollected && isTutorAddressInformationCollected && isTutorBankAccountInformationCollected && isTutorPersonalInformationCollected;

    const isIdentityVerificationInformationCollectedLoading =
        isGetAreTutorTermsOfServiceAcceptedLoading ||
        isGetAreVerificationDocumentsCollectedLoading ||
        isGetIsTutorAddressInformationCollectedLoading ||
        isGetIsTutorBankAccountInformationCollectedLoading ||
        isGetIsTutorPersonalInformationCollectedLoading;

    if (isIdentityVerificationInformationCollectedLoading) {
        return (
            <AuthenticationProtectedPage>
                <MainLayout>
                    <Center style={{ height: "100vh" }}>
                        <Loader size="xl" />
                    </Center>
                </MainLayout>
            </AuthenticationProtectedPage>
        );
    }

    return (
        <AuthenticationProtectedPage>
            <MainLayout>{isIdentityVerificationInformationCollected ? <PaymentsDashboard /> : <IdentityVerification />}</MainLayout>
        </AuthenticationProtectedPage>
    );
};

export default PaymentsPage;
