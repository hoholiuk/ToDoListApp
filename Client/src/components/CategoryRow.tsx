import React, {FC, ReactElement} from "react";
import Category from "../models/category";
import {BsTrash} from "react-icons/bs";
import {useDispatch} from "react-redux";
import {categoriesActions} from "../store/actions";

interface CategoryRowProps {
    category: Category;
}

const CategoryRow: FC<CategoryRowProps> = ({category}): ReactElement => {
    const dispatch = useDispatch();

    const handleDeleteButtonClick = () => {
        dispatch(categoriesActions.removeCategory(category.id));
    };

    return (
        <tr>
            <td className="col-11">{category.name}</td>
            <td className="col-1 px-3">
                <div className="d-flex justify-content-end">
                    <BsTrash onClick={handleDeleteButtonClick} className="actions"/>
                </div>
            </td>
        </tr>
    );
};

export default CategoryRow;
