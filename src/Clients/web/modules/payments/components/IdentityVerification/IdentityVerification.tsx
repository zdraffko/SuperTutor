import { Button, Group, Stack, Stepper } from "@mantine/core";
import { useState } from "react";
import AddressInformationStep from "./Steps/AddressInformationStep";
import PayoutInformationStep from "./Steps/PayoutInformationStep";
import PersonalInformationStep from "./Steps/PersonalInformationStep";

export const IdentityVerification: React.FC = () => {
    const [activeStep, setActiveStep] = useState(1);
    const nextStep = () => setActiveStep(current => (current < 3 ? current + 1 : current));
    const prevStep = () => setActiveStep(current => (current > 0 ? current - 1 : current));

    return (
        <Stack align="center" p="xl" justify="space-between" style={{ height: "100vh" }}>
            <Stepper active={activeStep} onStepClick={setActiveStep} breakpoint="sm" style={{ width: "60vw" }}>
                <Stepper.Step label="Стъпка 1" description="Лична информация" allowStepSelect={activeStep > 0}>
                    <PersonalInformationStep />
                </Stepper.Step>
                <Stepper.Step label="Стъпка 2" description="Адрес" allowStepSelect={activeStep > 1}>
                    <AddressInformationStep />
                </Stepper.Step>
                <Stepper.Step label="Стъпка 3" description="Изплащания" allowStepSelect={activeStep > 2}>
                    <PayoutInformationStep />
                </Stepper.Step>
                <Stepper.Completed>Completed, click back button to get to previous step</Stepper.Completed>
            </Stepper>

            <Group position="center">
                <Button variant="default" onClick={prevStep}>
                    Предишна стъпка
                </Button>
                <Button onClick={nextStep}>Следваща стъпка</Button>
            </Group>
        </Stack>
    );
};
