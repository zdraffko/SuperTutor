import { axios } from "utils/axios";

export interface UploadVerificationDocumentsRequest {
    identityDocumentFrontFile: File;
    identityDocumentBackFile: File;
    addressDocumentFile: File;
}

const uploadVerificationDocuments = async (request: UploadVerificationDocumentsRequest): Promise<void> => {
    const formData = new FormData();
    formData.append("identityDocumentFront", request.identityDocumentFrontFile);
    formData.append("identityDocumentBack", request.identityDocumentBackFile);
    formData.append("addressDocument", request.addressDocumentFile);

    await axios.post("/payments/UploadTutorVerificationDocuments", formData, {
        headers: {
            "Content-Type": "multipart/form-data"
        }
    });
};

export default uploadVerificationDocuments;
