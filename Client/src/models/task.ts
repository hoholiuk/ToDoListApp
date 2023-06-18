interface Task {
    id: number,
    title: string,
    isCompleted: boolean,
    dueDate: string | null,
    categoryId: number | null,
}

export default Task;
