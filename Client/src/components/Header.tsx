import React, {FC, ReactElement} from "react";

const Header: FC = (): ReactElement => {
    return (
        <div className="bg-white border-bottom shadow-sm mb-4">
            <div className="container d-flex justify-content-center">
                <span className="fs-1 logo text-decoration-none text-black my-2">ToDoList</span>
            </div>
            {/*<form method="post" className="d-flex justify-content-center mb-3 gap-2">*/}
            {/*    <select name="repository" className="form-select w-auto">*/}
            {/*        <option>SQL</option>*/}
            {/*        <option>XML</option>*/}
            {/*    </select>*/}
            {/*    <input type={"submit"} value="Change" className="btn btn-success"/>*/}
            {/*</form>*/}
        </div>
    )
}

export default Header
