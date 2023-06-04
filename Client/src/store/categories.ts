import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface Category {
    id: number;
    name: string;
}

let id = 0;

const categorySlice = createSlice({
    name: "categories",
    initialState: [] as Category[],
    reducers: {
        addCategory: (state, action: PayloadAction<{ name: string }>) => {
            state.push({
                id: ++id,
                name: action.payload.name,
            });
        },
        removeCategory: (state, action: PayloadAction<{ id: number }>) => {
            const index = state.findIndex((category) => category.id === action.payload.id);
            state.splice(index, 1);
        },
    },
});

export const { addCategory, removeCategory } = categorySlice.actions;
export default categorySlice.reducer;
