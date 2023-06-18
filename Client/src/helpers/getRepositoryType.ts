import {getCookie} from "typescript-cookie";
import {RepositoryTypes} from "../enums/repositoryTypes";

export const getRepositoryType = (): string => {
    const value = getCookie('repositoryType');

    if (typeof value === 'string' && value in RepositoryTypes) {
        return value;
    } else {
        return Object.values(RepositoryTypes)[0];
    }
}
