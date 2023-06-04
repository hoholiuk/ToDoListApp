import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import Task from "../types/task";

let id = 0;

const taskSlice = createSlice({
    name: "tasks",
    initialState: [] as Task[],
    reducers: {
        addTask: (state, action: PayloadAction<{ title: string; dueDate: Date | null; categoryId: number | null}>) => {
            state.unshift({
                id: ++id,
                title: action.payload.title,
                isCompleted: false,
                dueDate: action.payload.dueDate,
                categoryId: action.payload.categoryId,
            });
        },
        removeTask: (state, action: PayloadAction<{ id: number }>) => {
            const index = state.findIndex((task) => task.id === action.payload.id);
            state.splice(index, 1);
        },
        completeTask: (state, action: PayloadAction<{ id: number }>) => {
            const index = state.findIndex((task) => task.id === action.payload.id);
            state[index].isCompleted = !state[index].isCompleted;
        },
        updateTask: (
            state,
            action: PayloadAction<{ id: number; title: string; dueDate: Date | null; categoryId: number | null }>
        ) => {
            const index = state.findIndex((task) => task.id === action.payload.id);
            if (index >= 0) {
                state[index].title = action.payload.title;
                state[index].dueDate = action.payload.dueDate;
                state[index].categoryId = action.payload.categoryId;
            }
        },
    },
});

export const { addTask, removeTask, completeTask, updateTask } = taskSlice.actions;
export default taskSlice.reducer;
