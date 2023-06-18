import {getRepositoryType} from "../../../helpers/getRepositoryType";

export const fetchData = (query: string) => {
    return fetch('http://localhost:5201/graphql', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'repositoryType': getRepositoryType()
        },
        body: JSON.stringify({
            query: query,
        }),
    }).then(res => res.json());
}
