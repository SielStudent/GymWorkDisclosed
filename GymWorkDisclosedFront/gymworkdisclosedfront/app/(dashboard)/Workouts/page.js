"use client";
import React from 'react'
import GymGoer from '../../(dashboard)/Workouts/WorkoutList'
import {auth} from "@/app/components/firebase/firebase.Config";
import {getUser} from "@/app/components/services/userService";


export default async function Workouts() {
    const email = auth.currentUser.email;
    const token = await auth.currentUser.getIdToken();
    const user = await getUser(email, token)
    const filtervalue = "Arm"
    const filtertype = "bodypart"
    const filter = [filtervalue, filtertype, user]
    console.log("user after retrieval", user);
  return (
    <main>
      <nav>
        <div>
          <h2>Workouts</h2>
          <p><small>Workouts from user</small></p>
           
        </div>
      </nav>
        <GymGoer filter={filter}/>
        
       
    </main>
  )
} 