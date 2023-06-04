import {configureStore} from "@reduxjs/toolkit";
import taskReducer from "./tasks";
import categoryReducer from "./categories";

const store = configureStore({
    reducer: {
        tasks: taskReducer,
        categories: categoryReducer,
    },
    middleware: (getDefaultMiddleware) => [...getDefaultMiddleware()]
})

export default store
