import React, {FC, ReactElement} from "react";
import {TbEdit} from "react-icons/tb";
import {BsTrash} from "react-icons/bs";
import {useDispatch, useSelector} from "react-redux";
import Task from "../models/task";
import Category from "../models/category";
import {convertDateTimeToDate} from "../helpers/dateTimeToDateConverter";
import {tasksActions} from "../store/actions";

interface TaskRowProps {
    task: Task;
    setSelectedTask: React.Dispatch<React.SetStateAction<Task | null>>;
}

const TaskRow: FC<TaskRowProps> = ({task, setSelectedTask}): ReactElement => {
    const dispatch = useDispatch();
    const categories: Category[] = useSelector((state: any) => state.categories['categories']);
    const foundCategory: Category | undefined = categories.find((category: Category) => category.id === task.categoryId);

    const handleCheckboxClick = () => {
        dispatch(tasksActions.completeTask(task.id));
    };

    const handleDeleteButtonClick = () => {
        dispatch(tasksActions.removeTask(task.id));
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
            <td className="col-2">{task.dueDate ? convertDateTimeToDate(task.dueDate) : ""}</td>
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
