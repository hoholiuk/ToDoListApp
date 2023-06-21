import {getCookie} from "typescript-cookie";
import {RepositoryTypesEnum} from "../enums/repositoryTypes.enum";

export const getRepositoryType = (): string => {
    const value = getCookie('repositoryType');

    if (typeof value === 'string' && value in RepositoryTypesEnum) {
        return value;
    } else {
        return Object.values(RepositoryTypesEnum)[0];
    }
}
