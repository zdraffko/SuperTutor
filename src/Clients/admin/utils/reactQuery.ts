import { AxiosError } from "axios";
import { DefaultOptions, QueryClient, UseMutationOptions, UseQueryOptions } from "react-query";
import { PromiseValue } from "type-fest";

const defaultQueryOptions: DefaultOptions = {
    queries: {
        retry: 5
    }
};

export const queryClient = new QueryClient({ defaultOptions: defaultQueryOptions });

export type QueryConfig<FetcherFnType extends (...args: any) => any> = UseQueryOptions<PromiseValue<ReturnType<FetcherFnType>>>;

export type MutationConfig<FetcherFnType extends (...args: any) => any> = UseMutationOptions<PromiseValue<ReturnType<FetcherFnType>>, AxiosError, Parameters<FetcherFnType>[0]>;
