import {Epic, ofType} from 'redux-observable'
import {RootState} from "../storeConfig";
import {from, map, switchMap} from "rxjs";
import {tasksActions} from '../actions'
import Task from "../../models/task";
import {getTasksQuery, createTaskQuery, completeTaskQuery, updateTaskQuery, removeTaskQuery} from "./general/queries/taskQueries";
import {fetchData} from "./general/fetchData";

export const getTasksEpic: Epic<ReturnType<typeof tasksActions.getTasks>, any, RootState> = (action$, state$) => {
    return action$.pipe(
        ofType('GET_TASKS'),
        switchMap(action => from(fetchData(getTasksQuery()))),
        map(result => {
            const tasks = result.data.taskModelQuery.tasks;
            return tasksActions.setTasks(tasks);
        })
    );
}

export const addTaskEpic: Epic<ReturnType<typeof tasksActions.createTask>, any, RootState> = (action$, state$) => {
    return action$.pipe(
        ofType('CREATE_TASK'),
        switchMap(action => from(fetchData(createTaskQuery(action.payload)))),
        map(result => {
            const task: Task = result.data.taskModelMutation.createTask;
            return tasksActions.addTaskToState(task);
        })
    );
}

export const completeTaskEpic: Epic<ReturnType<typeof tasksActions.completeTask>, any, RootState> = (action$, state$) => {
    return action$.pipe(
        ofType('COMPLETE_TASK'),
        switchMap(action => from(fetchData(completeTaskQuery(action.payload)))),
        map(result => {
            const taskId: number = result.data.taskModelMutation.completeTask;
            return tasksActions.completeTaskInState(taskId);
        })
    );
}

export const updateTaskEpic: Epic<ReturnType<typeof tasksActions.updateTask>, any, RootState> = (action$, state$) => {
    return action$.pipe(
        ofType('UPDATE_TASK'),
        switchMap(action => from(fetchData(updateTaskQuery(action.payload)))),
        map(result => {
            const task: Task = result.data.taskModelMutation.updateTask;
            return tasksActions.updateTaskInState(task);
        })
    );
}

export const removeTaskEpic: Epic<ReturnType<typeof tasksActions.removeTask>, any, RootState> = (action$, state$) => {
    return action$.pipe(
        ofType('REMOVE_TASK'),
        switchMap(action => from(fetchData(removeTaskQuery(action.payload)))),
        map(result => {
            const taskId: number = result.data.taskModelMutation.deleteTask;
            return tasksActions.removeTaskFromState(taskId);
        })
    );
}
