import React from "react";
import { Link } from "react-router-dom";

interface Props {};

const Hero = (props: Props) => {
    return (
        <div id="hero" className="hero bg-base-200 min-h-screen">
            <div className="hero-content text-center">
                <div className="max-w-md">
                    <h1 className="text-5xl font-bold">
                        Financial data with no news.
                    </h1>
                    <p className="py-6">
                        Search relevant financial documents without fear mongering and fake news.
                    </p>
                    <button className="btn btn-primary">
                        <Link to="/search" className="">
                            Get Started
                        </Link>
                    </button>
                </div>
            </div>
        </div>
    )
}

export default Hero;