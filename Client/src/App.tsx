import React, {FC, ReactElement, useState} from 'react';
import Task from "./models/task";
import Header from "./components/Header";
import CategoryInputForm from "./components/CategoryInputForm";
import CategoriesTable from "./components/CategoriesTable";
import TaskInputForm from "./components/TaskInputForm";
import TasksTable from "./components/TasksTable";

const App: FC = (): ReactElement => {
    const [selectedTask, setSelectedTask] = useState<Task | null>(null);
    const [categoryFormVisibility, setCategoryFormVisibility] = useState<boolean>(false);

    return (
        <>
            <div>
                <Header/>
            </div>
            <div className="container">
                <main className="pb-3">
                    {categoryFormVisibility ? (
                        <>
                            <CategoryInputForm setCategoryFormVisibility={setCategoryFormVisibility}/>
                            <CategoriesTable/>
                        </>
                    ) : (
                        <>
                            <TaskInputForm
                                setCategoryFormVisibility={setCategoryFormVisibility}
                                selectedTask={selectedTask}
                                setSelectedTask={setSelectedTask}
                            />
                            <TasksTable setSelectedTask={setSelectedTask}/>
                        </>
                    )}
                </main>
            </div>
        </>
    );
};

export default App;
