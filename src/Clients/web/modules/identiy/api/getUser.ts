import { User } from "utils/authentication/types";
import { axios } from "utils/axios";

export const getUser = async (): Promise<User> => await axios.get("/identity");
