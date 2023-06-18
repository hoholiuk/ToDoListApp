import React, {FC, ReactElement} from "react";
import {useSelector} from "react-redux";
import CategoryRow from "./CategoryRow";
import Category from "../models/category";

const CategoriesTable: FC = (): ReactElement => {
    const categories: Category[] = useSelector((state: any) => state.categories['categories']);

    return (
        <>
            {categories.length > 0 && (
                <>
                    <table className="table mb-5">
                        <thead>
                        <tr>
                            <th>Name</th>
                            <th className="text-center">Actions</th>
                        </tr>
                        </thead>

                        <tbody>
                        {categories.map((category: Category) => (
                            <CategoryRow key={category.id} category={category}/>
                        ))}
                        </tbody>
                    </table>
                </>
            )}
        </>
    );
};

export default CategoriesTable;
