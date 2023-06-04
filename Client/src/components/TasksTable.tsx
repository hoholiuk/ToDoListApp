import React, {FC, ReactElement, useEffect, useState} from "react";
import {useSelector} from 'react-redux';
import TaskRow from "./TaskRow";
import Task from "../types/task";

interface TasksTableProps {
    setSelectedTask: React.Dispatch<React.SetStateAction<Task | null>>;
}

const TasksTable: FC<TasksTableProps> = ({setSelectedTask}): ReactElement => {
    const tasks = useSelector((state: { tasks: Task[] }) => state.tasks);
    const [numberUncompletedTasks, setNumberUncompletedTasks] = useState<number>(0);

    useEffect(() => {
        const uncompletedTasks = tasks.filter((task: Task) => !task.isCompleted);
        setNumberUncompletedTasks(uncompletedTasks.length);
    }, [tasks])

    const sortedTasks: Task[] = [...tasks].sort((a, b) => {
        if (a.dueDate && b.dueDate && !a.isCompleted && !b.isCompleted) {
            return new Date(a.dueDate).getTime() - new Date(b.dueDate).getTime();
        } else if (a.dueDate && !a.isCompleted) {
            return -1;
        } else if (b.dueDate && !b.isCompleted) {
            return 1;
        }
        return Number(a.isCompleted) - Number(b.isCompleted);
    });

    return (
        <>
            {tasks.length > 0 && (
                <>
                    <h3 className="d-flex justify-content-center mb-4">Active tasks: {numberUncompletedTasks}</h3>
                    <table className="table mb-5">
                        <thead>
                        <tr>
                            <th></th>
                            <th>Title</th>
                            <th>Category</th>
                            <th>Due date</th>
                            <th className="text-center">Actions</th>
                        </tr>
                        </thead>

                        <tbody>
                        {sortedTasks.map((task: Task) => (
                            <TaskRow key={task.id} task={task} setSelectedTask={setSelectedTask}/>
                        ))}
                        </tbody>
                    </table>
                </>
            )}
        </>
    );
}

export default TasksTable;
