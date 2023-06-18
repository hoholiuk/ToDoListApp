import React, {ReactElement, useState} from "react";
import {useDispatch} from "react-redux";
import {categoriesActions} from "../store/actions";
import CategoryInput from "../models/categoryInput";

interface CategoryInputFormProps {
    setCategoryFormVisibility: React.Dispatch<React.SetStateAction<boolean>>;
}

const CategoryInputForm: React.FC<CategoryInputFormProps> = ({setCategoryFormVisibility}): ReactElement => {
    const dispatch = useDispatch();
    const [name, setName] = useState<string>("");
    const [errorAlert, setErrorAlert] = useState<string>("");

    const handleBackButtonClick = () => {
        setCategoryFormVisibility(false);
    };

    const handleFormSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (name.trim().length < 1 || name.trim().length > 64) {
            setErrorAlert("Category name must be between 1 and 64 characters");
            return;
        }

        const category: CategoryInput = {
            name: name
        }

        dispatch(categoriesActions.createCategory(category));
        setErrorAlert("");
        setName("");
    };

    return (
        <form onSubmit={handleFormSubmit}
              className="d-flex flex-column justify-content-center align-items-center mb-4 pb-2">
            <div className="col-6 mt-4">
                <div className="form-outline d-flex flex-row gap-2">
                    <input
                        onClick={handleBackButtonClick}
                        value="Back"
                        type="button"
                        className="btn btn-info"
                    />
                    <input
                        value={name}
                        onChange={(event) => setName(event.target.value)}
                        type="text"
                        className="form-control"
                        placeholder="Enter a category here"
                    />
                    <input
                        value="Add"
                        type="submit"
                        className="form-control w-auto btn btn-success"
                    />
                </div>
                <span className="text-danger d-block text-center w-100 mt-1">{errorAlert}</span>
            </div>
        </form>
    );
};

export default CategoryInputForm;
