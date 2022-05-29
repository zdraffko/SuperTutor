import { Box, Button, Center, createStyles, InputWrapper, Paper, Text } from "@mantine/core";
import { Dropzone, MIME_TYPES } from "@mantine/dropzone";
import { useForm, zodResolver } from "@mantine/form";
import { showNotification } from "@mantine/notifications";
import useUploadVerificationDocuments from "modules/payments/hooks/useUploadVerificationDocuments";
import { useEffect } from "react";
import { X } from "tabler-icons-react";
import { z } from "zod";

const VerificationDocumentsFormSchema = z.object({
    identityDocumentFrontFile: z.instanceof(File, "Моля избери документ за верификация на самоличността (Отпред)"),
    identityDocumentBackFile: z.instanceof(File, "Моля избери документ за верификация на самоличността (Отзад)"),
    addressDocumentFile: z.instanceof(File, "Моля избери документ за верификация на адрес")
});

interface VerificationDocumentsStepProps {
    goToNextStep: () => void;
}

const VerificationDocumentsStep: React.FC<VerificationDocumentsStepProps> = ({ goToNextStep }) => {
    const { classes } = useStyles();
    const {
        uploadVerificationDocuments,
        isUploadVerificationDocumentsFailed,
        isUploadVerificationDocumentsLoading,
        isUploadVerificationDocumentsSuccessful,
        uploadVerificationDocumentsErrorMessage,
        resetUploadVerificationDocumentsRequestState
    } = useUploadVerificationDocuments();

    const formInitialValue: { identityDocumentFrontFile: File | undefined; identityDocumentBackFile: File | undefined; addressDocumentFile: File | undefined } = {
        identityDocumentFrontFile: undefined,
        identityDocumentBackFile: undefined,
        addressDocumentFile: undefined
    };

    const form = useForm({
        schema: zodResolver(VerificationDocumentsFormSchema),
        initialValues: formInitialValue
    });

    useEffect(() => {
        if (isUploadVerificationDocumentsSuccessful) {
            goToNextStep();
        }

        if (isUploadVerificationDocumentsFailed) {
            showNotification({
                autoClose: 5000,
                title: "Възникна проблем при запазването на данните",
                message: uploadVerificationDocumentsErrorMessage,
                color: "red",
                icon: <X />
            });
        }

        resetUploadVerificationDocumentsRequestState();
    }, [goToNextStep, isUploadVerificationDocumentsFailed, isUploadVerificationDocumentsSuccessful, resetUploadVerificationDocumentsRequestState, uploadVerificationDocumentsErrorMessage]);

    return (
        <Center m="xl">
            <Paper className={classes.formWrapper} shadow="sm" radius="md" p="md" withBorder>
                <form onSubmit={form.onSubmit(async values => await uploadVerificationDocuments(values))}>
                    <InputWrapper
                        p="sm"
                        label="Документ за верификация на самоличността (Отпред)"
                        description="Документи, които могат да се използват са: Паспорт, Лична карта, Свидетелство за управление на МПС, Разрешение за пребиваване"
                        required
                        {...form.getInputProps("identityDocumentFrontFile")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля избери документ за верификация на самоличността (Отпред)")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    >
                        <Dropzone
                            loading={isUploadVerificationDocumentsLoading}
                            multiple={false}
                            onDrop={files => form.setFieldValue("identityDocumentFrontFile", files[0])}
                            accept={[MIME_TYPES.png, MIME_TYPES.jpeg, MIME_TYPES.pdf]}
                        >
                            {() =>
                                form.values.identityDocumentFrontFile ? (
                                    <Text>{form.values.identityDocumentFrontFile.name}</Text>
                                ) : (
                                    <Text>Прикачи снимка или копие от предната страна на документа</Text>
                                )
                            }
                        </Dropzone>
                    </InputWrapper>
                    <InputWrapper
                        p="sm"
                        label="Документ за верификация на самоличността (Отзад)"
                        description="Документи, които могат да се използват са: Паспорт, Лична карта, Свидетелство за управление на МПС, Разрешение за пребиваване"
                        required
                        {...form.getInputProps("identityDocumentBackFile")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля избери документ за верификация на самоличността (Отзад)")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    >
                        <Dropzone
                            loading={isUploadVerificationDocumentsLoading}
                            multiple={false}
                            onDrop={files => form.setFieldValue("identityDocumentBackFile", files[0])}
                            accept={[MIME_TYPES.png, MIME_TYPES.jpeg, MIME_TYPES.pdf]}
                        >
                            {() =>
                                form.values.identityDocumentBackFile ? <Text>{form.values.identityDocumentBackFile.name}</Text> : <Text>Прикачи снимка или копие от задната страна на документа</Text>
                            }
                        </Dropzone>
                    </InputWrapper>
                    <InputWrapper
                        p="sm"
                        mb="xl"
                        label="Документ за верификация на адрес"
                        description="Документи, които могат да се използват са: Сметка за комунални услуги, Банково извлечение, Ипотечно извлечение"
                        required
                        {...form.getInputProps("addressDocumentFile")}
                        onInvalid={event => (event?.target as HTMLSelectElement).setCustomValidity("Моля избери документ за верификация на адрес")}
                        onInput={event => (event?.target as HTMLSelectElement).setCustomValidity("")}
                    >
                        <Dropzone
                            loading={isUploadVerificationDocumentsLoading}
                            multiple={false}
                            onDrop={files => form.setFieldValue("addressDocumentFile", files[0])}
                            accept={[MIME_TYPES.png, MIME_TYPES.jpeg, MIME_TYPES.pdf]}
                        >
                            {() => (form.values.addressDocumentFile ? <Text>{form.values.addressDocumentFile.name}</Text> : <Text>Прикачи снимка или копие на документ за верификация на адрес </Text>)}
                        </Dropzone>
                    </InputWrapper>
                    <Box p="sm" mt="xl">
                        <Center>
                            <Button type="submit" fullWidth size="sm" style={{ width: "50%" }} loading={isUploadVerificationDocumentsLoading}>
                                Запиши и продължи напред
                            </Button>
                        </Center>
                    </Box>
                </form>
            </Paper>
        </Center>
    );
};

const useStyles = createStyles(() => ({
    formWrapper: {
        width: "45vw"
    }
}));

export default VerificationDocumentsStep;
