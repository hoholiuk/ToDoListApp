import React, {FC, ReactElement} from "react";
import {TbEdit} from "react-icons/tb";
import {BsTrash} from "react-icons/bs";
import {completeTask, removeTask} from "../store/tasks";
import {useDispatch, useSelector} from "react-redux";
import Task from "../types/task";
import Category from "../types/category";
import {convertDateToString} from "../convertors/dateToStringConverter";

interface TaskRowProps {
    task: Task;
    setSelectedTask: React.Dispatch<React.SetStateAction<Task | null>>;
}

const TaskRow: FC<TaskRowProps> = ({task, setSelectedTask}): ReactElement => {
    const dispatch = useDispatch();
    const categories = useSelector((state: { categories: Category[] }) => state.categories);
    const foundCategory: Category | undefined = categories.find((category: Category) => category.id === task.categoryId);

    const handleCheckboxClick = () => {
        dispatch(completeTask({id: task.id}));
    };

    const handleDeleteButtonClick = () => {
        dispatch(removeTask({id: task.id}));
    };

    const handleEditButtonClick = () => {
        setSelectedTask(task);
    };

    return (
        <tr className={task.isCompleted ? "completed" : ""}>
            <td>
                <input
                    type="checkbox"
                    defaultChecked={task.isCompleted}
                    onClick={handleCheckboxClick}
                />
            </td>
            <td className={"col-7" + (task.isCompleted ? " text-decoration-line-through" : "")}>
                {task.title}
            </td>
            <td className="col-2">{foundCategory ? foundCategory.name : ""}</td>
            <td className="col-2">{task.dueDate ? convertDateToString(task.dueDate) : ""}</td>
            <td className="col-1 px-3">
                <div className={"d-flex justify-content" + (!task.isCompleted ? "-between" : "-end")}>
                    {!task.isCompleted && <TbEdit className="actions" onClick={handleEditButtonClick}/>}
                    <BsTrash onClick={handleDeleteButtonClick} className="actions"/>
                </div>
            </td>
        </tr>
    );
};

export default TaskRow;
