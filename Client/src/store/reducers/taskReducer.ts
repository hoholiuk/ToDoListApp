import Task from "../../models/task";
import {TaskActionTypes} from "../actions";
import {Reducer} from "react";
import {AnyAction} from "redux";

interface TaskState {
    tasks: Task[]
}

const initialState: TaskState = {
    tasks: []
}

export const taskReducer: Reducer<typeof initialState, TaskActionTypes> = (state: TaskState = initialState, action: AnyAction): TaskState => {
    switch (action.type) {
        case 'SET_TASKS':
            return {tasks: action.payload}
        case 'ADD_TASK_TO_STATE':
            return {tasks: [...state.tasks, action.payload as Task]};
        case 'COMPLETE_TASK_IN_STATE':
            let tasks = state.tasks.map((task) => {
                if (task.id === action.payload) {
                    return {...task, isCompleted: !task.isCompleted};
                }
                return task;
            });
            return {tasks: tasks}
        case 'REMOVE_TASK_FROM_STATE':
            let newState = [...state.tasks]
            newState = newState.filter((task) => task.id !== action.payload)
            return {tasks: [...newState]};
        case 'UPDATE_TASK_IN_STATE':
            let newTasks = state.tasks.map((task) => task.id === action.payload.id ? {
                id: action.payload.id,
                title: action.payload.title,
                dueDate: action.payload.dueDate,
                isCompleted: action.payload.isCompleted,
                categoryId: action.payload.categoryId,
            } : task);
            return {...state, tasks: [...newTasks]}
        default:
            return {...state};
    }
}
