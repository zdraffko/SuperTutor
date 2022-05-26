import { Stack, Stepper } from "@mantine/core";
import { useCallback, useState } from "react";
import AcceptTermsOfServiceStep from "./Steps/AcceptTermsOfServiceStep";
import AddressInformationStep from "./Steps/AddressInformationStep";
import PayoutInformationStep from "./Steps/PayoutInformationStep";
import PersonalInformationStep from "./Steps/PersonalInformationStep";
import VerificationDocumentsStep from "./Steps/VerificationDocumentsStep";

export const IdentityVerification: React.FC = () => {
    const numberOfSteps = 4;
    const [activeStep, setActiveStep] = useState(0);
    const goToNextStep = useCallback(() => setActiveStep(currentStep => (currentStep < numberOfSteps ? currentStep + 1 : currentStep)), []);

    return (
        <Stack align="center" p="xl" justify="space-between" style={{ height: "100vh" }}>
            <Stepper active={activeStep} onStepClick={setActiveStep} breakpoint="sm" style={{ width: "60vw" }}>
                <Stepper.Step label="Стъпка 1" description="Лична информация" allowStepSelect={false}>
                    <PersonalInformationStep goToNextStep={goToNextStep} />
                </Stepper.Step>
                <Stepper.Step label="Стъпка 2" description="Адрес" allowStepSelect={false}>
                    <AddressInformationStep goToNextStep={goToNextStep} />
                </Stepper.Step>
                <Stepper.Step label="Стъпка 3" description="Изплащания" allowStepSelect={true}>
                    <PayoutInformationStep goToNextStep={goToNextStep} />
                </Stepper.Step>
                <Stepper.Step label="Стъпка 4" description="Документи за верификация" allowStepSelect={true}>
                    <VerificationDocumentsStep goToNextStep={goToNextStep} />
                </Stepper.Step>
                <Stepper.Completed>
                    <AcceptTermsOfServiceStep />
                </Stepper.Completed>
            </Stepper>
        </Stack>
    );
};
