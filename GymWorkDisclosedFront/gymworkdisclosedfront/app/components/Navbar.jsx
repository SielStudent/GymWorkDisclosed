"use client";
import React, {useEffect} from 'react'
import Link from 'next/link'
import Login from "../components/auth/Login";
import Logout from "../components/auth/Logout";
import {auth} from "./firebase/firebase.config";

const LoggedInUser = () => {
    useEffect(() => {
        auth.onAuthStateChanged((user) => {
            if (user) {
                console.log("user is logged in", user)
                return(
                    <div>
                        <button onClick={Logout}> Logout </button>
                    </div>
                )
            } else {
                console.log("user is not logged in")
                return(
                    <div>
                        <button onClick={Login}> Login </button>
                    </div>
                )
            }
        })
    }, [])
}
export default function Navbar() {
    return (
        <nav>
            <h2>GymWorkDisclosed</h2>
            <Link href='/'> Home </Link>
            <Link href="/Workouts"> Workouts </Link>
            <Link href="/Workouts/PersonalBests"> Personal Bests </Link>
            <Link href="/Workouts/AddWorkout"> Add Workout </Link>
            <LoggedInUser/>
        </nav>
    )
}

