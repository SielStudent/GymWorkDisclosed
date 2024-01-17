"use client"
import Form from "../../../components/pagedata/AddWorkoutForm"
import React from "react";
import { auth } from "../../../components/firebase/firebase.config"
import { getUser } from "../../../components/services/userService";

export default async function Workouts() {
    const email = auth.currentUser.email;
    const token = await auth.currentUser.getIdToken();
    const user = await getUser(email, token)

    return (
        <main>
            <Form user={user} idtoken={token}/>
        </main>
    )
}