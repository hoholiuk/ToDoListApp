interface Task {
    id: number,
    title: string,
    isCompleted: boolean,
    dueDate: Date | null,
    categoryId: number | null,
}

export default Task;
