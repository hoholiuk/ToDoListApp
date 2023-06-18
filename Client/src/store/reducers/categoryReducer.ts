import {CategoryActionTypes} from "../actions";
import {Reducer} from "react";
import {AnyAction} from "redux";
import Category from "../../models/category";

interface CategoryState {
    categories: Category[]
}

const initialState: CategoryState = {
    categories: []
}

export const categoryReducer: Reducer<typeof initialState, CategoryActionTypes> = (state: CategoryState = initialState, action: AnyAction): CategoryState => {
    switch (action.type) {
        case 'SET_CATEGORIES':
            return {categories: action.payload}
        case 'ADD_CATEGORY_TO_STATE':
            return {categories: [...state.categories, action.payload as Category]};
        case 'REMOVE_CATEGORY_FROM_STATE':
            let newState = [...state.categories]
            newState = newState.filter((category) => category.id !== action.payload)
            return {categories: [...newState]};
        default:
            return {...state};
    }
}
