import React, {FC, ReactElement, useEffect, useState} from "react";
import {useDispatch} from "react-redux";
import {categoriesActions, tasksActions} from "../store/actions";
import {setCookie} from 'typescript-cookie';
import {getRepositoryType} from "../helpers/getRepositoryType";
import {RepositoryTypes} from "../enums/repositoryTypes";

const Header: FC = (): ReactElement => {
    const dispatch = useDispatch();
    const [repositoryType, setRepositoryType] = useState<string>(getRepositoryType());

    useEffect(() => {
        setCookie('repositoryType', repositoryType, {expires: 7})
        dispatch(tasksActions.getTasks());
        dispatch(categoriesActions.getCategories());
    }, [repositoryType, dispatch]);

    return (
        <div className="bg-white border-bottom shadow-sm mb-4">
            <div className="container d-flex justify-content-center">
                <span className="fs-1 logo text-decoration-none text-black my-2">ToDoList</span>
            </div>
            <form method="post" className="d-flex justify-content-center mb-3 gap-2">
                <label className="d-flex justify-content-center align-items-center">Repository type: </label>
                <select name="repositoryType" className="form-select w-auto"
                        value={repositoryType}
                        onChange={(event) => setRepositoryType(event.target.value)}>

                    {Object.values(RepositoryTypes).map((type) => (
                        <option key={type} value={type}>
                            {type}
                        </option>
                    ))}
                </select>
            </form>
        </div>
    )
}

export default Header
