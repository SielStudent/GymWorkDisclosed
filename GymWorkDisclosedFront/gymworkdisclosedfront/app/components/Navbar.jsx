"use client";
import React, {useEffect, useState} from 'react'
import Link from 'next/link'
import Login from "../components/auth/Login";
import Logout from "../components/auth/Logout";
import {auth} from "./firebase/firebase.config";

const LoggedInUser = () => {
    const [authNav, setAuthNav] = useState()
    useEffect(() => {
        auth.onAuthStateChanged((user) => {
            if (user) {
                console.log("user is logged in", user)
                setAuthNav(
                <nav>
                    <h2>GymWorkDisclosed</h2>
                    <Link href='/'> Home </Link>
                    <Link href="/Workouts"> Workouts </Link>
                    <Link href="/Workouts/PersonalBests"> Personal Bests </Link>
                    <Link href="/Workouts/AddWorkout"> Add Workout </Link>
                    <button className="btn-primary" onClick={Logout}>Logout</button>
                </nav>)
            } 
            else {
                console.log("user is not logged in")
                setAuthNav(
                    <nav>
                        <h2>GymWorkDisclosed</h2>
                        <Link href='/'> Home </Link>
                        <button className="btn-primary" onClick={Login}> Login </button>
                    </nav>
                )
            }
        })
    }, [])
    return authNav
}
export default function Navbar() {
    return (
        <div>
            <LoggedInUser/>
        </div>
    )
}

