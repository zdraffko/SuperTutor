import { AxiosError } from "axios";
import { useQuery } from "react-query";
import getAreTutorTermsOfServiceAccepted, { GetAreTutorTermsOfServiceAcceptedResponse } from "../api/getAreTutorTermsOfServiceAccepted";

const useGetAreTutorTermsOfServiceAccepted = () => {
    const query = useQuery<GetAreTutorTermsOfServiceAcceptedResponse, AxiosError<string>>("payments-getAreTutorTermsOfServiceAccepted", getAreTutorTermsOfServiceAccepted, { staleTime: 5000 });

    return {
        areTutorTermsOfServiceAccepted: query.data?.areTermsOfServiceAccepted,
        isGetAreTutorTermsOfServiceAcceptedLoading: query.isLoading,
        isGetAreTutorTermsOfServiceAcceptedSuccessful: query.isSuccess,
        isGetAreTutorTermsOfServiceAcceptedFailed: query.isError,
        getAreTutorTermsOfServiceAcceptedErrorMessage: typeof query.error?.response?.data === typeof String ? query.error?.response?.data : "Неочаквана грешка. Опитай пак по-късно."
    };
};

export default useGetAreTutorTermsOfServiceAccepted;
