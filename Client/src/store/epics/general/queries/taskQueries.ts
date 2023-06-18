import Task from "../../../../models/task";
import TaskInput from "../../../../models/taskInput";

export const getTasksQuery = () => {
    return `
        query {
          taskModelQuery {
            tasks {
              id
              title
              isCompleted
              dueDate
              categoryId
            }
          }
        }
    `;
};

export const createTaskQuery = (task: TaskInput) => {
    task.dueDate = task.dueDate ? `"${task.dueDate}T00:00:00"` : null;

    return `
        mutation {
          taskModelMutation {
            createTask(task: {
                title: "${task.title}",
                dueDate: ${task.dueDate},
                categoryId: ${task.categoryId}
            }) {
              id
              title
              isCompleted
              dueDate
              categoryId
            }
          }
        }
    `;
}

export const completeTaskQuery = (id: number) => {
    return `
        mutation {
          taskModelMutation {
            completeTask(id: ${id})
          }
        }
    `;
}

export const updateTaskQuery = (task: Task) => {
    task.categoryId = task.categoryId === 0 ? null : task.categoryId

    return `
        mutation {
          taskModelMutation {
            updateTask(task: {
              id: ${task.id},
              title: "${task.title}",
              dueDate: ${task.dueDate ? `"${task.dueDate}T00:00:00"` : null},
              categoryId: ${task.categoryId}
          }) {
              id
              title
              isCompleted
              dueDate
              categoryId
            }
          }
        }
    `;
}

export const removeTaskQuery = (id: number) => {
    return `
        mutation {
          taskModelMutation {
            deleteTask(id: ${id})
          }
        }
    `;
}
