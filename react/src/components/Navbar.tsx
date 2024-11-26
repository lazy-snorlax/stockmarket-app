import React from "react";
import { Link } from "react-router-dom";

interface Props {}

const Navbar = (props: Props) => {
    return (
        <div className="navbar bg-base-100">
            <div className="flex-1">
                <Link to="/">Stock Market App</Link>
            </div>
            <div className="flex-none">
                <ul className="menu menu-horizontal px-1">
                    <li>
                        <Link to="/login">Login</Link>
                    </li>
                    <li>
                        <Link to="/register">Register</Link>
                    </li>
                </ul>
            </div>
        </div>
    )
}

export default Navbar