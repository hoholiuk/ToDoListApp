import {Epic, ofType} from 'redux-observable'
import {RootState} from "../storeConfig";
import {from, map, switchMap} from "rxjs";
import {categoriesActions} from '../actions'
import Category from "../../models/category";
import {getCategoriesQuery, createCategoryQuery, removeCategoryQuery} from "./general/queries/categoryQueries";
import {fetchData} from "./general/fetchData";

export const getCategoriesEpic: Epic<ReturnType<typeof categoriesActions.getCategories>, any, RootState> = (action$, state$) => {
    return action$.pipe(
        ofType('GET_CATEGORIES'),
        switchMap(action => from(fetchData(getCategoriesQuery()))),
        map(result => {
            const categories = result.data.categoryModelQuery.categories;
            return categoriesActions.setCategories(categories);
        })
    );
}

export const addCategoriesEpic: Epic<ReturnType<typeof categoriesActions.createCategory>, any, RootState> = (action$, state$) => {
    return action$.pipe(
        ofType('CREATE_CATEGORY'),
        switchMap(action => from(fetchData(createCategoryQuery(action.payload)))),
        map(result => {
            const category: Category = result.data.categoryModelMutation.createCategory;
            return categoriesActions.addCategoryToState(category);
        })
    );
}

export const removeCategoryEpic: Epic<ReturnType<typeof categoriesActions.removeCategory>, any, RootState> = (action$, state$) => {
    return action$.pipe(
        ofType('REMOVE_CATEGORY'),
        switchMap(action => from(fetchData(removeCategoryQuery(action.payload)))),
        map(result => {
            const categoryId: number = result.data.categoryModelMutation.deleteCategory;
            return categoriesActions.removeCategoryFromState(categoryId);
        })
    );
}
