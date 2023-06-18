import Task from "../models/task";
import TaskInput from "../models/taskInput";
import Category from "../models/category";
import CategoryInput from "../models/categoryInput";

export const tasksActions = {
    getTasks: () => ({
        type: 'GET_TASKS',
        payload: null
    } as const),
    setTasks: (tasks: Task[]) => ({
        type: 'SET_TASKS',
        payload: tasks
    } as const),
    createTask: (task: TaskInput) => ({
        type: 'CREATE_TASK',
        payload: task
    } as const),
    addTaskToState: (task: Task) => ({
        type: 'ADD_TASK_TO_STATE',
        payload: task
    } as const),
    completeTask: (id: number) => ({
        type: 'COMPLETE_TASK',
        payload: id
    } as const),
    completeTaskInState: (id: number) => ({
        type: 'COMPLETE_TASK_IN_STATE',
        payload: id
    } as const),
    removeTask: (id: number) => ({
        type: 'REMOVE_TASK',
        payload: id
    } as const),
    removeTaskFromState: (id: number) => ({
        type: 'REMOVE_TASK_FROM_STATE',
        payload: id
    } as const),
    updateTask: (task: Task) => ({
        type: 'UPDATE_TASK',
        payload: task
    } as const),
    updateTaskInState: (task: Task) => ({
        type: 'UPDATE_TASK_IN_STATE',
        payload: task
    } as const),
}

export const categoriesActions = {
    getCategories: () => ({
        type: 'GET_CATEGORIES',
        payload: null
    } as const),
    setCategories: (categories: Category[]) => ({
        type: 'SET_CATEGORIES',
        payload: categories
    } as const),
    createCategory: (category: CategoryInput) => ({
        type: 'CREATE_CATEGORY',
        payload: category
    } as const),
    addCategoryToState: (category: Category) => ({
        type: 'ADD_CATEGORY_TO_STATE',
        payload: category
    } as const),
    removeCategory: (id: number) => ({
        type: 'REMOVE_CATEGORY',
        payload: id
    } as const),
    removeCategoryFromState: (id: number) => ({
        type: 'REMOVE_CATEGORY_FROM_STATE',
        payload: id
    } as const),
}

type ValueOf<T> = T[keyof T]

export type TaskActionCreatorType = ValueOf<typeof tasksActions>
export type TaskActionTypes = ReturnType<TaskActionCreatorType>

export type CategoryActionCreatorType = ValueOf<typeof categoriesActions>
export type CategoryActionTypes = ReturnType<CategoryActionCreatorType>
