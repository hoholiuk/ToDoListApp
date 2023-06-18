import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {taskReducer} from "./reducers/taskReducer";
import {categoryReducer} from "./reducers/categoryReducer";
import {addTaskEpic, completeTaskEpic, getTasksEpic, removeTaskEpic, updateTaskEpic} from "./epics/taskEpics";
import {combineEpics, createEpicMiddleware} from 'redux-observable'
import {addCategoriesEpic, getCategoriesEpic, removeCategoryEpic} from "./epics/categoryEpics";

const epicMiddleware = createEpicMiddleware();

// @ts-ignore
const rootEpic = combineEpics(getTasksEpic, getCategoriesEpic, addTaskEpic, completeTaskEpic, removeTaskEpic, updateTaskEpic, addCategoriesEpic, removeCategoryEpic);

const rootReducer = combineReducers({
    tasks: taskReducer,
    categories: categoryReducer
})

const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(epicMiddleware),
    devTools: true
});

// @ts-ignore
epicMiddleware.run(rootEpic);

export type RootState = ReturnType<typeof store.getState>;

export default store
