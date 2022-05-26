import { Center, Loader, Stack, Stepper } from "@mantine/core";
import useGetAreTutorTermsOfServiceAccepted from "modules/payments/hooks/useGetAreTutorTermsOfServiceAccepted";
import useGetAreTutorVerificationDocumentsCollected from "modules/payments/hooks/useGetAreTutorVerificationDocumentsCollected";
import useGetIsTutorAddressInformationCollected from "modules/payments/hooks/useGetIsTutorAddressInformationCollected";
import useGetIsTutorBankAccountInformationCollected from "modules/payments/hooks/useGetIsTutorBankAccountInformationCollected";
import useGetIsTutorPersonalInformationCollected from "modules/payments/hooks/useGetIsTutorPersonalInformationCollected";
import { useCallback, useState } from "react";
import AcceptTermsOfServiceStep from "./Steps/AcceptTermsOfServiceStep";
import AddressInformationStep from "./Steps/AddressInformationStep";
import PayoutInformationStep from "./Steps/PayoutInformationStep";
import PersonalInformationStep from "./Steps/PersonalInformationStep";
import VerificationDocumentsStep from "./Steps/VerificationDocumentsStep";

export const IdentityVerification: React.FC = () => {
    const { areTutorTermsOfServiceAccepted, isGetAreTutorTermsOfServiceAcceptedLoading } = useGetAreTutorTermsOfServiceAccepted();
    const { areVerificationDocumentsCollected, isGetAreVerificationDocumentsCollectedLoading } = useGetAreTutorVerificationDocumentsCollected();
    const { isTutorAddressInformationCollected, isGetIsTutorAddressInformationCollectedLoading } = useGetIsTutorAddressInformationCollected();
    const { isTutorBankAccountInformationCollected, isGetIsTutorBankAccountInformationCollectedLoading } = useGetIsTutorBankAccountInformationCollected();
    const { isTutorPersonalInformationCollected, isGetIsTutorPersonalInformationCollectedLoading } = useGetIsTutorPersonalInformationCollected();

    const isIdentityVerificationInformationCollectedLoading =
        isGetAreTutorTermsOfServiceAcceptedLoading ||
        isGetAreVerificationDocumentsCollectedLoading ||
        isGetIsTutorAddressInformationCollectedLoading ||
        isGetIsTutorBankAccountInformationCollectedLoading ||
        isGetIsTutorPersonalInformationCollectedLoading;

    let currentActiveStep = 0;
    if (isTutorPersonalInformationCollected) {
        currentActiveStep = 1;
    }

    if (isTutorAddressInformationCollected) {
        currentActiveStep = 2;
    }

    if (isTutorBankAccountInformationCollected) {
        currentActiveStep = 3;
    }

    if (areVerificationDocumentsCollected) {
        currentActiveStep = 4;
    }

    const numberOfSteps = 4;
    const [activeStep, setActiveStep] = useState(currentActiveStep);
    const goToNextStep = useCallback(() => setActiveStep(currentStep => (currentStep < numberOfSteps ? currentStep + 1 : currentStep)), []);

    if (isIdentityVerificationInformationCollectedLoading) {
        return (
            <Center style={{ height: "100vh" }}>
                <Loader size="xl" />
            </Center>
        );
    }

    return (
        <Stack align="center" p="xl" justify="space-between" style={{ height: "100vh" }}>
            <Stepper active={activeStep} onStepClick={setActiveStep} breakpoint="sm" style={{ width: "60vw" }}>
                <Stepper.Step label="Стъпка 1" description="Лична информация" allowStepSelect={false}>
                    <PersonalInformationStep goToNextStep={goToNextStep} />
                </Stepper.Step>
                <Stepper.Step label="Стъпка 2" description="Адрес" allowStepSelect={false}>
                    <AddressInformationStep goToNextStep={goToNextStep} />
                </Stepper.Step>
                <Stepper.Step label="Стъпка 3" description="Изплащания" allowStepSelect={false}>
                    <PayoutInformationStep goToNextStep={goToNextStep} />
                </Stepper.Step>
                <Stepper.Step label="Стъпка 4" description="Документи за верификация" allowStepSelect={false}>
                    <VerificationDocumentsStep goToNextStep={goToNextStep} />
                </Stepper.Step>
                <Stepper.Completed>
                    <AcceptTermsOfServiceStep />
                </Stepper.Completed>
            </Stepper>
        </Stack>
    );
};
