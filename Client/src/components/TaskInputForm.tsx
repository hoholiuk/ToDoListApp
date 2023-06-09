import React, {FC, ReactElement, useState, useEffect} from "react";
import {useDispatch, useSelector} from "react-redux";
import Category from "../models/category";
import Task from "../models/task";
import {convertDateTimeToDate} from "../helpers/dateTimeToDateConverter";
import {tasksActions} from "../store/actions";
import TaskInput from "../models/taskInput";

interface TaskInputFormProps {
    setCategoryFormVisibility: React.Dispatch<React.SetStateAction<boolean>>;
    setSelectedTask: React.Dispatch<React.SetStateAction<Task | null>>;
    selectedTask: Task | null;
}

const TaskInputForm: FC<TaskInputFormProps> = ({setCategoryFormVisibility, selectedTask, setSelectedTask}): ReactElement => {
    const dispatch = useDispatch();
    const categories: Category[] = useSelector((state: any) => state.categories['categories']);
    const [title, setTitle] = useState<string>("");
    const [dueDate, setDueDate] = useState<string | null>(null);
    const [categoryId, setCategoryId] = useState<number | null>(null);
    const [buttonValue, setButtonValue] = useState<string>("Add");
    const [buttonColor, setButtonColor] = useState<string>("btn-success");
    const [errorAlert, setErrorAlert] = useState<string>("");

    useEffect(() => {
        if (selectedTask) {
            setTitle(selectedTask.title);
            setDueDate(convertDateTimeToDate(selectedTask.dueDate));
            setCategoryId(selectedTask.categoryId);
            setButtonValue("Save");
            setButtonColor("btn-warning");
        } else {
            setTitle("");
            setDueDate(null);
            setCategoryId(null);
            setButtonValue("Add");
            setButtonColor("btn-success");
        }
    }, [selectedTask]);

    const handleFormSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (title.trim().length < 1 || title.trim().length > 128) {
            setErrorAlert("Task title must be between 1 and 128 characters");
            return;
        }

        if (selectedTask) {
            const task: Task = {
                id: selectedTask.id,
                title: title,
                dueDate: dueDate,
                isCompleted: false,
                categoryId: categoryId
            }
            dispatch(tasksActions.updateTask(task));
            setSelectedTask(null);
        } else {
            const task: TaskInput = {
                title: title,
                dueDate: dueDate,
                categoryId: categoryId
            }
            dispatch(tasksActions.createTask(task));
        }

        setTitle("");
        setDueDate(null);
        setCategoryId(null);
        setErrorAlert("");
    };

    const handleAddCategoryButtonClick = () => {
        setCategoryFormVisibility(true);
    };

    return (
        <form
            onSubmit={handleFormSubmit}
            className="d-flex flex-column justify-content-center align-items-center mb-4 pb-2"
        >
            <div className="col-6 mt-4">
                <span className="text-danger"></span>
                <div className="form-outline d-flex flex-row gap-2">
                    <input
                        value={title}
                        onChange={(event) => setTitle(event.target.value)}
                        type="text"
                        className="form-control"
                        placeholder="Enter a task here"
                    />
                    <input
                        value={buttonValue}
                        className={"form-control w-auto btn " + buttonColor}
                        type="submit"
                    />
                </div>
            </div>
            <div className="col-6 row justify-content-around">
                <div className="col-5 mt-4 p-0 d-flex gap-2">
                    <select
                        value={categoryId || ""}
                        onChange={(event) => setCategoryId(Number(event.target.value))}
                        className="form-select"
                    >
                        <option value="">Default category</option>
                        {categories.map((category: Category) => (
                            <option key={category.id} value={category.id}>
                                {category.name}
                            </option>
                        ))}
                    </select>
                    <input
                        onClick={handleAddCategoryButtonClick}
                        value="+"
                        type="button"
                        title="Add category"
                        className="form-control w-auto text-decoration-none text-black"
                    />
                </div>
                <div className="col-5 mt-4 p-0">
                    <input
                        value={dueDate ? dueDate : ""}
                        onChange={(event) => {
                            setDueDate(event.target.value === "" ? null : event.target.value);
                        }}
                        type={"date"}
                        className="form-control"
                    />
                </div>
            </div>
            <span className="text-danger d-block text-center w-100 mt-3">{errorAlert}</span>
        </form>
    );
}

export default TaskInputForm;
